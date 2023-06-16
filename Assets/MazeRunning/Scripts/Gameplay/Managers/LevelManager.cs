using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MazeRunning.Gameplay.Maze.Generator;
using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Sound;
using MazeRunning.Utils.Collections;
using Zenject;

namespace MazeRunning.Gameplay.Managers
{
    public class LevelManager : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly MazeGenerator _mazeGenerator;
        private readonly GenMazeInfoProvider _genMazeInfoProvider;
        private readonly AudioManager _audioManager;
        private GamePhase _gamePhase;
        public bool IsRunning => _gamePhase == GamePhase.Running;
        private int _currentLevel;
        private readonly Dictionary<PlayerType, int> _playerToScore = new(EnumUtilities.Count<PlayerType>());
        public LevelManager(MazeGenerator mazeGenerator, AudioManager audioManager, GenMazeInfoProvider provider, SignalBus signalBus)
        {
            _audioManager = audioManager;
            _mazeGenerator = mazeGenerator;
            _genMazeInfoProvider = provider;
            _gamePhase = GamePhase.Constructing;
            _signalBus = signalBus;
            _currentLevel = Units.FirstLevel;
            //Init default score for all players
            foreach (PlayerType e in Enum.GetValues(typeof(PlayerType)))
                _playerToScore.Add(e, default);
        }
        private async UniTaskVoid StartLevel()
        {
            _gamePhase = GamePhase.Constructing;
            _signalBus.Fire(new StartLevelSignal
            {
                Scores = _playerToScore,
                CurrentLevel = _currentLevel
            });
            //Generate in another task
            await _mazeGenerator.Generate(_genMazeInfoProvider.Provide(_currentLevel));
            _gamePhase = GamePhase.Running;
        }
        public void ApplyNextLevel()
        {
            _currentLevel++;
            StartLevel().Forget();
        }
        public void RequestWin(PlayerType type)
        {
            if (!IsRunning || !_playerToScore.TryGetValue(type, out var score))
                return;
            _gamePhase = GamePhase.EndGame;
            _playerToScore[type] += score + Units.ScoreOffset;
            _audioManager.PlayVfx(type == PlayerType.Player ? SoundName.WinGame : SoundName.LoseGame);
            _signalBus.Fire(new EndLevelSignal
            {
                Winner = type
            });
        }
        public void Initialize()
        {
            StartLevel().Forget();
        }
    }
}