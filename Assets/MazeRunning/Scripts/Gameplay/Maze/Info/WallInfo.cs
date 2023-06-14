using UnityEngine;

namespace MazeRunning.Gameplay.Maze.Info
{
    public readonly struct WallInfo
    {
        public Direction Direction { get; init; }
        public Vector3 Position { get; init; }
    }
}