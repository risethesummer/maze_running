using MazeRunning.Gameplay.Managers;
using MazeRunning.Gameplay.Maze.Environment;
using MazeRunning.Gameplay.Maze.Generator;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Sound;
using MazeRunning.Utils.Pool;
using MazeRunning.Utils.Task;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private GameObject doorPrefab;
        [SerializeField] private int initialWallCount = 500;
        [SerializeField] private GenMazeInfo genMazeInfo;
        [SerializeField] private AudioManager audioManager;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<StartLevelSignal>();
            Container.DeclareSignal<DoneGeneratingMaze>();
            Container.DeclareSignal<EndLevelSignal>();
            Container.Bind<CancellationTokenTracker>().FromNew().AsTransient();
            
            Container.BindInstance(genMazeInfo);
            Container.Bind<GenMazeInfoProvider>().FromNew().AsSingle();
            Container.Bind<MazeDrawer>().FromNew().AsSingle();
            Container.Bind<MazeGenerator>().FromNew().AsSingle();
            Container.Bind<ObjectsHolder>().FromNew().AsSingle();
            Container.BindInstance(audioManager).AsSingle();
            Container.BindFactory<WallInfo, MazeWall, MazeWall.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(initialWallCount).FromComponentInNewPrefab(wallPrefab));
            Container.BindFactory<DoorInfo, Door, Door.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(1).FromComponentInNewPrefab(doorPrefab));
            
            Container.BindInterfacesAndSelfTo<LevelManager>().FromNew().AsSingle().NonLazy();

        }
    }
}