using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using MazeRunning.Utils.Physics;

namespace MazeRunning.Gameplay.Maze.Generator
{
    public class MazeDrawer
    {
        private readonly MazeWall.Factory _wallFactory;
        private readonly Door.Factory _doorFactory;

        public MazeDrawer(MazeWall.Factory wallFactory, Door.Factory doorFactory)
        {
            _wallFactory = wallFactory;
            _doorFactory = doorFactory;
        }

        private static bool CheckShouldSpawnAtDirection(Direction direction, GenMazeInfo info, PairIndex index)
        {
            return direction switch
            {
                Direction.Left or Direction.Top => true,
                //Should not spawn right or bottom wall in the other sides but right and bottom side
                Direction.Right => index.Col == info.Width - 1,
                Direction.Bottom => index.Row == 0,
                _ => true
            };
        }
        public async IAsyncEnumerable<IDisposable> Draw(GenMazeInfo info, Maze maze)
        {
            var delayDuration = TimeSpan.FromSeconds(info.DelayEachGenInSecond);
            foreach (var index in maze.AllIndexes)
            {
                var cell = maze.GetCellAt(index);
                //var position = new Vector3(-info.Width / 2 + index.Row, 0, -info.Height / 2 + index.Col);
                var position = info.GetPositionOfCellAt(index);
                var states = cell.DirectionStates;
                for (int i = 0; i < states.Length; i++)
                {
                    if (!states[i])
                        continue;
                    var direction = (Direction)i;
                    if (!CheckShouldSpawnAtDirection(direction, info, index))
                    {
                        continue;
                    }
                    var wallPos = position + direction.GetOffsetInCell(info.CellSize);
                    //var rotation = direction.GetLookRotation();
                    var wall = _wallFactory.Create(new WallInfo
                    {
                        Direction = direction,
                        Position = wallPos,
                        Index = index
                    });
                    yield return wall;
                    if (info.DelayEachGenInSecond > 0)
                        await UniTask.Delay(delayDuration);
                }
            }
            yield return _doorFactory.Create(maze.DoorInfo);
        }
    }
}