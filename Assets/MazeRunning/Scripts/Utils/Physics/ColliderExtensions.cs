using MazeRunning.SharedStructures.Data;
using UnityEngine;
using UnityEngine.AI;

namespace MazeRunning.Utils.Physics
{
    public static class ColliderExtensions
    {
        private const string PlayerTag = "Player";
        private const string BotTag = "Bot";
        public static PlayerType? GetPlayerType(this Collider col)
        {
            if (col.CompareTag(PlayerTag))
                return PlayerType.Player;
            if (col.CompareTag(BotTag))
                return PlayerType.AI;
            return null;
        }

        public static bool HasReachDestination(this NavMeshAgent agent)
        {
            if (agent.pathPending) return false;
            if (agent.remainingDistance > agent.stoppingDistance) 
                return false;
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }
    }
}