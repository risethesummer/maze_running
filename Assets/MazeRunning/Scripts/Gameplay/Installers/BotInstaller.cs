using MazeRunning.Gameplay.Active;
using MazeRunning.Gameplay.Camera;
using MazeRunning.Gameplay.Movement;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace MazeRunning.Gameplay.Installers
{
    public class BotInstaller : MonoInstaller
    {
        [SerializeField] private BotCameraController cameraController;
        [SerializeField] private Transform model;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private int minSilly;
        [SerializeField] private int maxSilly;
        public override void InstallBindings()
        {
            Container.Bind<NavMeshAgent>().FromComponentOnRoot().AsSingle();
            Container.BindInterfacesAndSelfTo<BotMovementController>().FromNew().AsSingle();
            Container.BindInstance(cameraController).AsSingle();
            Container.BindInstance(model).AsSingle();
            Container.Bind<RotationController>().FromNew().AsSingle().WithArguments(turnSpeed, rotateSpeed);
            Container.Bind<BotActiveController>().FromNew().AsSingle().NonLazy();
            Container.Bind<BotSillyController>().FromNew().AsSingle().WithArguments(minSilly, maxSilly);
        }
    }
}