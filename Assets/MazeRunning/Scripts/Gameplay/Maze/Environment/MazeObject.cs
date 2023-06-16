using System;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Environment
{
    public abstract class MazeObject<T> : MonoBehaviour, IPoolable<T, IMemoryPool>, IDisposable
    {
        protected IMemoryPool Pool;
        public void OnDespawned()
        {
            Pool = null;
        }
        public virtual void OnSpawned(T info, IMemoryPool pool)
        {
            Pool = pool;
        }
        public void Dispose()
        {
            Pool.Despawn(this);
        }

    }
}