using System;
using MazeRunning.Gameplay.Maze.Info;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Environment
{
    public class MazeWall : MonoBehaviour, IPoolable<WallInfo, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(WallInfo info, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = info.Position;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
        public class Factory : PlaceholderFactory<WallInfo, MazeWall>
        {
        }
    }
}