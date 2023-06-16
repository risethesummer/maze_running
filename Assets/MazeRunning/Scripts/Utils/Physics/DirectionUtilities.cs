using System;
using System.Collections.Generic;
using MazeRunning.Gameplay.Maze.Generator;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeRunning.Utils.Physics
{
    public static class DirectionUtilities
    {
        public static readonly Direction[] Directions = {
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
        // private static readonly PairIndex[] NeighborOffsetIndexes = {
        //     new() {Row = 0, Col = -1 },
        //     new() {Row = 1, Col = 0 },
        //     new() {Row = 0, Col = 1 },
        //     new() {Row = -1, Col = 0 }
        // };
        private static readonly PairIndex[] NeighborOffsetIndexes = {
            new() {Row = 0, Col = -1 },
            new() {Row = 1, Col = 0 },
            new() {Row = 0, Col = 1 },
            new() {Row = -1, Col = 0 }
        };
        private static readonly Vector2[] PositionOffsetInCellDirections = {
            Vector2.right, 
            Vector2.down,
            Vector2.left,
            Vector2.up,
        };

        public static readonly int Count = Enum.GetNames(typeof(Direction)).Length;

        public static int GetInt(this Direction direction) => (int)direction;
        public static Direction GetOpposite(this Direction direction)
        {
            return OppositeDirections[direction.GetInt()];
        }

        private static readonly Vector3[] OffsetsInCell = {
            Vector3.left,
            Vector3.forward,
            Vector3.right,
            Vector3.back
        };
        public static Vector3 GetOffsetInCell(this Direction direction, float cellSize)
        {
            return OffsetsInCell[direction.GetInt()] * (cellSize / 2f);
        }

        public static Quaternion GetLookRotation(this Direction direction)
        {
            var dir = PositionOffsetInCellDirections[direction.GetInt()];
            return Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
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
        public static PairIndex GetBorderRandomIndex(this Direction bias, int height, int width)
        {
            return bias switch
            {
                Direction.Left => new PairIndex { Row = Random.Range(0, height), Col = 0},
                Direction.Top => new PairIndex { Row = height - 1, Col = Random.Range(0, width) },
                Direction.Right => new PairIndex { Row = Random.Range(0, height), Col = width - 1  },
                Direction.Bottom => new PairIndex { Row = 0, Col = Random.Range(0, width) },
                _ => throw new ArgumentOutOfRangeException(nameof(bias), bias, null)
            };
        }
        public static PairIndex GetNeighborIndexOffset(this Direction direction)
        {
            return NeighborOffsetIndexes[direction.GetInt()];
        }
    }
}