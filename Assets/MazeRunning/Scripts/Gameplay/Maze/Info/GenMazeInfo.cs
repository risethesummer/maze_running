using UnityEngine;

namespace MazeRunning.Gameplay.Maze.Info
{
    public struct GenMazeInfo
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public Vector3 MazeCenter { get; init; }
        public Direction OutWayBias { get; init; }
    }
}