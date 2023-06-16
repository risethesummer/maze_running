using MazeRunning.Gameplay.Maze.Info;

namespace MazeRunning.Gameplay.Managers
{
    public class GenMazeInfoProvider
    {
        private readonly GenMazeInfo _baseInfo;

        public GenMazeInfoProvider(GenMazeInfo baseInfo)
        {
            _baseInfo = baseInfo;
        }
        public GenMazeInfo Provide(int level)
        {
            _baseInfo.SetScale(level);
            return _baseInfo;
        }
    }
}