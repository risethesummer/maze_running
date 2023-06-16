using System.Collections.Generic;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Data;
using UnityEngine;

namespace MazeRunning.SharedStructures.Signals
{
    public readonly struct DoneGeneratingMaze
    {
        public DoorInfo DoorInfo { get; init; }
        public IDictionary<PlayerType, Vector3> PlayerPositions { get; init; }
        public GenMazeInfo GenMazeInfo { get; init; }
    }
}