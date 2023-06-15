using MazeRunning.Gameplay.Managers;
using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Generator;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Signals;
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
            Container.DeclareSignal<StartLevelSignal>();
            Container.DeclareSignal<DoneGeneratingMaze>();
            Container.DeclareSignal<EndLevelSignal>();
            Container.Bind<CancellationTokenTracker>().FromNew().AsTransient();
            Container.Bind<GenMazeInfoProvider>().FromNew().AsSingle();
            Container.Bind<MazeGenerator>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelManager>().FromNew().AsSingle().NonLazy();
            Container.BindFactory<WallInfo, MazeWall, MazeWall.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(initialWallCount).FromComponentInNewPrefab(wallPrefab));
        }
    }
}