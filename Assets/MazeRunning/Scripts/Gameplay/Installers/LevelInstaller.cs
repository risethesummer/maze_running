using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Generator;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.Utils.Task;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private int initialWallCount = 200;
        public override void InstallBindings()
        {
            Container.Bind<CancellationTokenTracker>().FromNew().AsTransient();
            Container.Bind<MazeGenerator>().FromNew().AsSingle();
            Container.BindFactory<WallInfo, MazeWall, MazeWall.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(initialWallCount).FromComponentInNewPrefab(wallPrefab));
        }
    }
}