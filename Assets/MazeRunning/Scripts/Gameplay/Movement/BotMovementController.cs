using System;
using MazeRunning.Utils.Physics;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace MazeRunning.Gameplay.Movement
{
    public class BotMovementController : IMovementController, IFixedTickable
    {
        protected readonly NavMeshAgent Agent;
        private readonly RotationController _rotationController;
        public event Action OnReachDestination;
        public BotMovementController(NavMeshAgent agent, RotationController rotationController)
        {
            Agent = agent;
            Agent.updateRotation = false;
            _rotationController = rotationController;
        }
        public virtual void MoveTo(Vector3 position)
        {
            Stop();
            Agent.isStopped = false;
            Agent.SetDestination(position);
        }

        public void HardSet(Vector3 position)
        {
            Agent.enabled = false;
            Agent.transform.position = position;
            Agent.enabled = true;
        }

        public void Stop()
        {
            Agent.isStopped = true;
            Agent.ResetPath();
        }

        public void FixedTick()
        {
            var vel = Agent.velocity;
            if (vel != Vector3.zero)
                _rotationController.LookRotation(Agent.velocity);
            if (!Agent.HasReachDestination()) 
                return;
            OnReachDestination?.Invoke();
        }
    }
}