using System.Collections.Generic;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using MazeRunning.Utils.Physics;

namespace MazeRunning.Gameplay.Maze.Generator
{
    public readonly struct Maze
    {
        public struct MazeCell
        {
            public readonly bool[] DirectionStates;
            public bool HasVisited;

            public MazeCell(bool hasVisited, bool[] directionStates)
            {
                HasVisited = hasVisited;
                DirectionStates = directionStates;
            }

            public void RemoveWallAtDirection(Direction direction)
            {
                DirectionStates[direction.GetInt()] = false;
            }
        }
        
        public readonly struct Neighbor
        {
            public PairIndex Index { get; init; }
            public Direction Direction { get; init; }
            public bool Valid { get; init; }
        }

        private readonly MazeCell[,] _cells;
        private readonly PairIndex[] _allIndexes;
        public DoorInfo DoorInfo { get; }
        public IEnumerable<PairIndex> AllIndexes => _allIndexes;
        public ref MazeCell GetCellAt(PairIndex index) => ref _cells[index.Row, index.Col];

        public void RemoveWallAtDirection(PairIndex index, Direction direction)
        {
            ref var cell = ref GetCellAt(index);
            cell.RemoveWallAtDirection(direction);
        }
        public void MarkCellAsVisited(in PairIndex pairIndex)
        {
            ref var mazeCell = ref GetCellAt(pairIndex);
            mazeCell.HasVisited = true;
        }
        public Maze(GenMazeInfo genMazeInfo)
        {
            _cells = new MazeCell[genMazeInfo.Width, genMazeInfo.Height];
            _allIndexes = new PairIndex[genMazeInfo.Width * genMazeInfo.Height];
            for (var i = 0; i < genMazeInfo.Width; i++)
            {
                for (var j = 0; j < genMazeInfo.Height; j++)
                {
                    //Try to init a cell with 4 sides
                    //And remove the sides when visiting each cell (if needed)
                    _cells[i, j] = new MazeCell(false, new[] { true, true, true, true });
                    _allIndexes[i * genMazeInfo.Width + j] = new PairIndex
                    {
                        Row = i, Col = j
                    };
                }
            }
            DoorInfo = genMazeInfo.GetOutDoorPosition();
            RemoveWallAtDirection(DoorInfo.Index, DoorInfo.Direction);
        }
    }
}