using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using UnityEngine;

namespace MazeRunning.Gameplay.Maze.Info
{
    public struct WallInfo
    {
        public Direction Direction { get; init; }
        public Vector3 Position { get; init; }
        public PairIndex Index { get; init; }
    }
}