using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using MazeRunning.Utils.Physics;

namespace MazeRunning.Gameplay.Maze.Generator
{
    /// <summary>
    /// Facade to setup data before generating maze mesh
    /// </summary>
    public class MazeGenPreparer
    {
        public static MazePreparation PreGenerate(GenMazeInfo genMazeInfo)
        {
            var mazeTempInfo = new MazePreparation(genMazeInfo);
            var remainingWalls = mazeTempInfo.GenFirstCell();
            while (remainingWalls.Count > 0)
                BackTrackingInternal(ref mazeTempInfo, remainingWalls);
            return mazeTempInfo;
        }

        /// <summary>
        /// Use backtracking algorithm to recursively generate each wall
        /// </summary>
        /// <param name="mazePreparation"></param>
        /// <param name="remainingCells"></param>
        private static void BackTrackingInternal(ref MazePreparation mazePreparation, Stack<PairIndex> remainingCells)
        {
            var peekCellIndex = remainingCells.Peek();
            if (mazePreparation.IsCellFull(peekCellIndex))
            {
                remainingCells.Pop();
                return;
            }
            var shouldProcessNeighbor = mazePreparation.GenAndSetWall(peekCellIndex);
            if (shouldProcessNeighbor.HasValue)
                remainingCells.Push(shouldProcessNeighbor.Value);
        }

        public readonly ref struct MazePreparation
        {
            public class CellInfo
            {
                private const int BufferLength = 4;
                [FixedBuffer(typeof(bool), BufferLength)]
                public StateBuffer DirectionStates;
                public struct StateBuffer
                {
                    public bool Left;
                    public bool Top;
                    public bool Right;
                    public bool Bottom;
                    public int CountNotGen()
                    {
                        return Left.GetReverseInt() + Top.GetReverseInt() + Right.GetReverseInt() + Bottom.GetReverseInt();
                    }
                }
                
                public Direction RandomNotGenDirection
                {
                    get
                    {
                        var notGenCount = DirectionStates.CountNotGen();
                        Span<int> notGenIndexes = stackalloc int[notGenCount];
                        var j = 0;
                        for (var i = 0; i < BufferLength; i++)
                        {
                            if (!DirectionStates[i])
                                notGenIndexes[j++] = i;
                        }

                        return (Direction)notGenIndexes.RandomElement();
                    }
                }
            }

            private readonly CellInfo[,] _cells;
            private readonly GenMazeInfo _genMazeInfo;

            public MazePreparation(GenMazeInfo genMazeInfo)
            {
                _genMazeInfo = genMazeInfo;
                _cells = new CellInfo[genMazeInfo.Width, genMazeInfo.Height];
                _cells.Init2DArray(genMazeInfo.Width, genMazeInfo.Height);
            }

            /// <summary>
            /// Try to define the top left corner cell
            /// </summary>
            /// <returns></returns>
            public Stack<PairIndex> GenFirstCell()
            {
                var firstCell = _cells[0, 0];
                firstCell.DirectionStates[Direction.Left.GetInt()] = true;
                firstCell.DirectionStates[Direction.Top.GetInt()] = true;
                var remainingCells = new Stack<PairIndex>(_cells.Length);
                remainingCells.Push(new PairIndex());
                return remainingCells;
            }


            /// <summary>
            /// Gen a new wall associated with this cell
            /// </summary>
            /// <param name="index"></param>
            /// <returns>Neighbor index</returns>
            public PairIndex? GenAndSetWall(PairIndex index)
            {
                var cell = _cells[index.Row, index.Col];
                var randDir = cell.RandomNotGenDirection;
                return SetCellHasWallAtDirection(index, randDir);
            }

            /// <summary>
            /// Set the wall at the cell and its associated neighbor
            /// </summary>
            /// <param name="index"></param>
            /// <param name="direction"></param>
            public PairIndex? SetCellHasWallAtDirection(PairIndex index, Direction direction)
            {
                var cellInfo = _cells[index.Row, index.Col];
                cellInfo.DirectionStates[direction.GetInt()] = true;

                //Try to find a neighbor and modify its state
                var neighborIndex = direction.GetNeighborIndexOffset();
                //No neighbor
                if (!neighborIndex.IsValidIndex(_genMazeInfo.Width, _genMazeInfo.Height))
                    return null;
                var neighBorInfo = _cells[neighborIndex.Row, neighborIndex.Col];
                neighBorInfo.DirectionStates[direction.GetOpposite().GetInt()] = true;
                return neighborIndex;
            }

            //Check a cell has no more space for generating new walls
            public bool IsCellFull(PairIndex index)
            {
                var cellInfo = _cells[index.Row, index.Col];
                return cellInfo.DirectionStates.Count(e => e) >= DirectionUtilities.Count - 1;
            }
        }
    }
}