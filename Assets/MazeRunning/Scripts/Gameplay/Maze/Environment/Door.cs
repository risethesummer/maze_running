using System;
using MazeRunning.Gameplay.Managers;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.Utils.Physics;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Maze.Environment
{
    public class Door : MonoBehaviour, IPoolable<DoorInfo, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private LevelManager _levelManager;
        [Inject]
        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        public void OnDespawned()
        {
            _pool = null;
        }
        public void OnSpawned(DoorInfo info, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = info.Position;
        }
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        public class Factory : PlaceholderFactory<DoorInfo, Door>
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_levelManager.IsRunning)
            {
                var player = other.GetPlayerType();
                if (player.HasValue)
                    _levelManager.RequestWin(player.Value);
            }
        }
    }
}