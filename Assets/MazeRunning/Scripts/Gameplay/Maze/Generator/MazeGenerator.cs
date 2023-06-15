using System;
using Cysharp.Threading.Tasks;
using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Utils.Debug;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Generator
{
    public class MazeGenerator
    {
        private readonly MazeWall.Factory _wallFactory;
        private readonly SignalBus _signalBus;
        public MazeGenerator(MazeWall.Factory wallFactory, SignalBus signalBus)
        {
            _wallFactory = wallFactory;
            _signalBus = signalBus;
        }
        public UniTaskVoid Generate(GenMazeInfo genMazeInfo)
        {
            try
            {
                var prepareInfo = MazeGenPreparer.PreGenerate(genMazeInfo);
                CreateWallsFromPreparation(genMazeInfo, prepareInfo);
            }
            catch (Exception e)
            {
                this.LogException(e);
            }
            _signalBus.Fire<DoneGeneratingMaze>();
            return default;
        }

        private void CreateWallsFromPreparation(GenMazeInfo genMazeInfo, MazeGenPreparer.MazePreparation preparation)
        {
            
        }
    }
}