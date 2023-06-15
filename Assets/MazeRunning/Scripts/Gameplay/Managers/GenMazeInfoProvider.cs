using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.Utils.Collections;

namespace MazeRunning.Gameplay.Managers
{
    public class GenMazeInfoProvider
    {
        public GenMazeInfo Provide(int level)
        {
            var mazeLength = Units.BaseLength * level;
            return new GenMazeInfo
            {
                Width = mazeLength,
                Height = mazeLength,
            };
        }
    }
}