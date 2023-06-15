using System;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;

namespace MazeRunning.Utils.Physics
{
    public static class DirectionUtilities
    {
        private static readonly Direction[] Directions = {
            Direction.Left,
            Direction.Top,
            Direction.Right,
            Direction.Bottom,
        };
        private static readonly Direction[] OppositeDirections = {
            Direction.Right,
            Direction.Bottom,
            Direction.Left,
            Direction.Top
        };
        private static readonly PairIndex[] OffsetIndexes = {
            new() {Row = -1, Col = 0 },
            new() {Row = 0, Col = 1 },
            new() {Row = 1, Col = 0 },
            new() {Row = 0, Col = -1 }
        };
        public static readonly int Count = Enum.GetNames(typeof(Direction)).Length;

        public static int GetInt(this Direction direction) => (int)direction;
        public static Direction GetOpposite(this Direction direction)
        {
            return OppositeDirections[direction.GetInt()];
        }
        public static bool IsValidIndex(this PairIndex index, int rowLength, int colLength)
        {
            var row = index.Row;
            var col = index.Col;
            return row >= 0 && 
                   row < rowLength && 
                   col >= 0 && 
                   col < colLength;
        }
        public static Direction GetRandomDirection() => Directions[UnityEngine.Random.Range(0, Directions.Length)];
        public static PairIndex GetNeighborIndexOffset(this Direction direction)
        {
            return OffsetIndexes[direction.GetInt()];
        }
    }
}