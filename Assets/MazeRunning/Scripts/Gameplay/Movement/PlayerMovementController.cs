using UnityEngine;
using UnityEngine.AI;

namespace MazeRunning.Gameplay.Movement
{
    public class PlayerMovementController
    {
        private readonly NavMeshAgent _agent;
        public PlayerMovementController(NavMeshAgent agent)
        {
            _agent = agent;
        }
        public void MoveTo(Vector3 position)
        {
            _agent.SetDestination(position);
        }
    }
}