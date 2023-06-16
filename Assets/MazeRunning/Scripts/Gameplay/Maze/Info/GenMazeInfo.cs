using System;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using MazeRunning.Utils.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeRunning.Gameplay.Maze.Info
{
    [Serializable]
    public class GenMazeInfo
    {
        public int Width { get;  private set; }
        public int Height { get; private set; }
        public float Dense { get; private set; }
        [SerializeField, Header("The base size of maze")] private int baseSize = 0;
        [SerializeField, Header("Scale size through each level")] private float scaleSize = 10;
        [SerializeField, Header("The base density of maze")] private float baseDense = 0.7f;
        [SerializeField, Header("Scale density through each level")] private float scaleDense = 0.1f;
        public void SetScale(int level)
        {
            Width = Height = (int)(baseSize + scaleSize * level);
            Dense = baseDense + scaleDense * level;
        } 
        /// <summary>
        /// Where is the top bottom cell of the maze is take place
        /// </summary>
        [field: SerializeField] public Vector3 BottomLeftPosition { get; private set; }
        [field: SerializeField] public float DelayEachGenInSecond { get; private set; }
        /// <summary>
        /// The size of each cell including four walls
        /// Assume that each cell is a square
        /// </summary>
        [field: SerializeField] public float CellSize { get; private set; }
        /// <summary>
        /// Which direction the in door should appear
        /// </summary>
        [field: SerializeField] public Direction InBias { get; private set; }
        /// <summary>
        /// Which direction the out door should appear
        /// </summary>
        [field: SerializeField] public Direction OutBias { get; private set; }

        /// <summary>
        /// Get the position of a cell relative to current maze info
        /// </summary>
        /// <returns>The relative position ref to <see cref="BottomLeftPosition"/></returns>
        public Vector3 GetPositionOfCellAt(in PairIndex cellIndex)
        {
            return BottomLeftPosition + new Vector3(-Width / 2f + cellIndex.Col * CellSize, 0, -Height / 2f + cellIndex.Row * CellSize);
        }
        
        public Vector3 GetRandomPositionInMaze()
        {
            var cellIndex = PairIndex.RandomRange(Width, Height);
            return GetPositionOfCellAt(cellIndex);
        }

        public DoorInfo GetInDoorPosition() => GetDoorInfo(InBias, false);
        private DoorInfo GetDoorInfo(Direction bias, bool outDoor)
        {
            var index = bias.GetBorderRandomIndex(Height, Width);
            var pos = GetPositionOfCellAt(index);
            return new DoorInfo
            {
                Direction = bias,
                Index = index,
                Position = pos,
                IsOutDoor = outDoor
            }; 
        }
        public DoorInfo GetOutDoorPosition() => GetDoorInfo(OutBias, true);
    }
}