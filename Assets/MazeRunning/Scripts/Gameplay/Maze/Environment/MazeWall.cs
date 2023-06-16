using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Physics;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Environment
{
    public class MazeWall : MazeObject<WallInfo>
    {
        public Direction Direction;

        public override void OnSpawned(WallInfo info, IMemoryPool pool)
        {
            base.OnSpawned(info, pool);
            transform.position = info.Position;
            Direction = info.Direction;
            gameObject.name = $"Wall {info.Index.Row} {info.Index.Col}";
            transform.rotation = info.Direction.GetLookRotation();
        }
        
        public class Factory : PlaceholderFactory<WallInfo, MazeWall>
        {
        }
    }
}