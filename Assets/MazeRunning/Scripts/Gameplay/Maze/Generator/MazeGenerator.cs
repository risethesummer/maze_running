using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Utils.Collections;
using MazeRunning.Utils.Debug;
using MazeRunning.Utils.Physics;
using MazeRunning.Utils.Pool;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Generator
{
    public class MazeGenerator
    {
        private readonly Door.Factory _doorFactory;
        private readonly SignalBus _signalBus;
        private readonly MazeDrawer _drawer;
        private readonly ObjectsHolder _holder;
        public MazeGenerator(SignalBus signalBus, 
            MazeDrawer drawer, ObjectsHolder holder)
        {
            _signalBus = signalBus;
            _drawer = drawer;
            _holder = holder;
        }

        public async UniTask Generate(GenMazeInfo genMazeInfo)
        {
            try
            {
                _holder.ReleaseAll();
                var maze = new Maze(genMazeInfo);
                Backtracking(ref maze, genMazeInfo);
                await foreach (var mazeObj in _drawer.Draw(genMazeInfo, maze))
                {
                    _holder.Hold(mazeObj);
                }
                _signalBus.Fire(new DoneGeneratingMaze
                {
                    PlayerPositions = new Dictionary<PlayerType, Vector3>
                    {
                        { PlayerType.Player , genMazeInfo.GetInDoorPosition().Position },
                        { PlayerType.AI , genMazeInfo.GetInDoorPosition().Position },
                    },
                    DoorInfo = maze.DoorInfo,
                    GenMazeInfo = genMazeInfo
                });
            }
            catch (Exception e)
            {
                this.LogException(e);
            }
        }
        // private DoorInfo[] MakeDoors(ref Maze maze, GenMazeInfo genMazeInfo)
        // {
        //     var doors = new DoorInfo[DoorInfo.TotalDoor];
        //     doors[DoorInfo.OutDoorIndex] = genMazeInfo.GetOutDoorPosition();
        //     doors[DoorInfo.PlayerInDoorIndex] = genMazeInfo.GetInDoorPosition();
        //     doors[DoorInfo.BotInDoorIndex] = genMazeInfo.GetInDoorPosition();
        //     maze.RemoveWallAtDirection(doors[DoorInfo.OutDoorIndex].Index);
        //     return doors;
        // }
        
        /// <summary>
        /// Apply backtracking algorithm to generate maze information without too much allocation
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="genMazeInfo"></param>
        private void Backtracking(ref Maze maze, GenMazeInfo genMazeInfo)
        {
            var remainingCellIndexes = new Stack<PairIndex>();
            //Pick a random initial cell
            var randCellIndex = PairIndex.RandomRange(genMazeInfo.Width, genMazeInfo.Height);
            maze.MarkCellAsVisited(randCellIndex);
            remainingCellIndexes.Push(randCellIndex);
            Span<Maze.Neighbor> neighbours = stackalloc Maze.Neighbor[DirectionUtilities.Count];
            //Try to backtrack all the cells in the maze
            while (remainingCellIndexes.Count > 0)
            {
                var currentIndex = remainingCellIndexes.Pop();
                //Find all valid neighbors
                var countNeighbor = GetNeighbors(currentIndex, maze, genMazeInfo, ref neighbours);
                if (countNeighbor <= 0) 
                    continue;
                //Get a random unvisited neighbor
                var randNeighbour = RandomValidNeighbor(ref neighbours, countNeighbor);
                var neighborIndex = randNeighbour.Index;
                //Remove the shared wall between 2 cells
                maze.RemoveWallAtDirection(currentIndex, randNeighbour.Direction);
                maze.RemoveWallAtDirection(neighborIndex, randNeighbour.Direction.GetOpposite());
                maze.MarkCellAsVisited(neighborIndex);
                //Prior to check the neighbor but still storing the old cell for backtracking
                remainingCellIndexes.Push(currentIndex);
                remainingCellIndexes.Push(neighborIndex);
            }
        }

        private Maze.Neighbor RandomValidNeighbor(ref Span<Maze.Neighbor> neighbors, int countValid)
        {
            Span<Maze.Neighbor> validNeighbor = stackalloc Maze.Neighbor[countValid];
            var j = 0;
            for (var i = 0; i < neighbors.Length; i++)
            {
                ref var neighBor = ref neighbors[i];
                if (neighBor.Valid)
                    validNeighbor[j++] = neighBor;
            }
            return validNeighbor.RandomElement();
        }
        
        /// <summary>
        /// Get all unvisited neighbors of a cell
        /// </summary>
        /// <param name="selfIndex"></param>
        /// <param name="maze"></param>
        /// <param name="info"></param>
        /// <param name="neighbors"></param>
        /// <returns></returns>
        private int GetNeighbors(PairIndex selfIndex, in Maze maze, GenMazeInfo info, ref Span<Maze.Neighbor> neighbors)
        {
            var total = 0;
            foreach (var direction in DirectionUtilities.Directions)
            {
                var neighborIndex = selfIndex + direction.GetNeighborIndexOffset();
                //If the neighbor has not been visited
                
                var valid = neighborIndex.IsValidIndex(info.Height, info.Width) &&
                            !maze.GetCellAt(neighborIndex).HasVisited;
                if (valid)
                    total++;
                neighbors[direction.GetInt()] = new Maze.Neighbor
                {
                    Index = neighborIndex,
                    Valid = valid,
                    Direction = direction
                };
            }
            return total;
        }
    }
}