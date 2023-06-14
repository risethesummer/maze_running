using MazeRunning.Utils.Physics;
using UnityEngine;

namespace MazeRunning.Gameplay.Maze.Info
{
    public struct GenMazeInfo
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public Vector3 MazeTopLeftPosition { get; init; }
        public float CellSize;
        public float WallThickness;
        public Direction InBias { get; init; }
        public Direction OutBias { get; init; }
    }
}