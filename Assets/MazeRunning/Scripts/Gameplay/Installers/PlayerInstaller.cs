using MazeRunning.Gameplay.Camera;
using MazeRunning.Gameplay.Input;
using MazeRunning.Gameplay.Movement;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace MazeRunning.Gameplay.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCameraManager cameraManager;
        public override void InstallBindings()
        {
            Container.Bind<MazeRunningInputActions>().FromNew().AsSingle();
            Container.Bind<InputHandlingManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<NavMeshAgent>().FromComponentOnRoot().AsSingle();
            Container.BindInstance(cameraManager).AsSingle();
            //Container.Bind<PlayerMovementController>().FromNew().AsSingle().NonLazy();
        }
    }
}