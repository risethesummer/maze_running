using MazeRunning.Gameplay.Active;
using MazeRunning.Gameplay.Camera;
using MazeRunning.Gameplay.Input;
using MazeRunning.Gameplay.Movement;
using MazeRunning.SharedStructures.Data;
using MazeRunning.Sound;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCameraManager cameraManager;
        [SerializeField] private Transform rotateComp;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float minDistance = 0.1f;
        [SerializeField] private float maxDistance = 1f;
        [SerializeField] private float turnSpeed = 0.1f;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float delaySound;
        public override void InstallBindings()
        {
            Container.BindInstance(PlayerType.Player);
            Container.Bind<MazeRunningInputActions>().FromNew().AsSingle();
            Container.Bind<InputHandlingManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<BaseActiveController>().FromNew().AsSingle();
            Container.Bind<CharacterController>().FromComponentOnRoot().AsSingle();
            Container.BindInstance(rotateComp).AsSingle().WhenInjectedInto<RotationController>();
            Container.Bind<RotationController>().FromNew().AsSingle().WithArguments(turnSpeed, rotateSpeed);
            Container.Bind<DelaySound>().FromNew().AsSingle().WithArguments(delaySound);
            Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromNew().AsSingle().WithArguments(speed, minDistance, maxDistance);
            Container.BindInstance(cameraManager).AsSingle();
            //Container.Bind<PlayerMovementController>().FromNew().AsSingle().NonLazy();
        }
    }
}