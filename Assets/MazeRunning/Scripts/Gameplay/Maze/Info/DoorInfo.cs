using System;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using UnityEngine;

namespace MazeRunning.Gameplay.Maze.Info
{
    public struct DoorInfo : IComparable<DoorInfo>
    {
        // public const int OutDoorIndex = 0;
        // public const int PlayerInDoorIndex = 1;
        // public const int BotInDoorIndex = 2;
        // public const int TotalDoor = 3;
        public PairIndex Index { get; init; }
        public Direction Direction { get; init; }
        public Vector3 Position { get; set; }
        public bool IsOutDoor { get; init; }
        public int CompareTo(DoorInfo other)
        {
            var compareIndex = Index.CompareTo(other.Index);
            return compareIndex != 0 ? compareIndex : Direction.CompareTo(other.Direction);
        }
    }
}