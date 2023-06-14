using Cysharp.Threading.Tasks;
using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Info;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Generator
{
    public readonly struct DoneGeneratingMaze
    {
        
    }
    public class MazeGenerator
    {
        private readonly MazeWall.Factory _wallFactory;
        private readonly MazeGenPreparer _genPreparer;
        private readonly SignalBus _signalBus;
        public MazeGenerator(MazeWall.Factory wallFactory, MazeGenPreparer mazeGenPreparer, SignalBus signalBus)
        {
            _wallFactory = wallFactory;
            _genPreparer = mazeGenPreparer;
            _signalBus = signalBus;
        }

        public UniTaskVoid Generate(GenMazeInfo genMazeInfo)
        {
            var prepareInfo = _genPreparer.PreGenerate(genMazeInfo);
            _signalBus.Fire<DoneGeneratingMaze>();
            return default;
        }

        private void CreateWallsFromPreparation(GenMazeInfo genMazeInfo, MazeGenPreparer.MazePreparation preparation)
        {
            
        }
    }
}