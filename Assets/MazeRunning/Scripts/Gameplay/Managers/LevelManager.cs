using System;
using System.Collections.Generic;
using MazeRunning.Gameplay.Maze.Generator;
using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Utils.Collections;
using Zenject;

namespace MazeRunning.Gameplay.Managers
{
    public class LevelManager : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly MazeGenerator _mazeGenerator;
        private readonly GenMazeInfoProvider _genMazeInfoProvider;
        private GamePhase _gamePhase;
        public bool IsRunning => _gamePhase == GamePhase.Running;
        private int _currentLevel;
        private readonly Dictionary<PlayerType, int> _playerToScore = new(EnumUtilities.Count<PlayerType>());
        public LevelManager(MazeGenerator mazeGenerator, GenMazeInfoProvider provider, SignalBus signalBus)
        {
            _mazeGenerator = mazeGenerator;
            _genMazeInfoProvider = provider;
            _gamePhase = GamePhase.Constructing;
            _signalBus = signalBus;
            _currentLevel = Units.FirstLevel;
            //Init default score for all players
            foreach (PlayerType e in Enum.GetValues(typeof(PlayerType)))
                _playerToScore.Add(e, default);
        }
        private void StartLevel()
        {
            _gamePhase = GamePhase.Constructing;
            _signalBus.Fire(new StartLevelSignal
            {
                Scores = _playerToScore,
                CurrentLevel = _currentLevel
            });
            //Generate in another task
            _mazeGenerator.Generate(_genMazeInfoProvider.Provide(_currentLevel)).Forget();
        }
        public void ApplyNextLevel()
        {
            _currentLevel++;
            StartLevel();
        }
        public void RequestWin(PlayerType type)
        {
            if (!_playerToScore.TryGetValue(type, out var score))
                return;
            _gamePhase = GamePhase.UI;
            _playerToScore[type] += score + Units.ScoreOffset;
            _signalBus.Fire(new EndLevelSignal
            {
                Winner = type
            });
        }
        public void Initialize()
        {
            StartLevel();
        }
    }
}