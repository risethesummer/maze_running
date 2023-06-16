using UnityEngine;

namespace MazeRunning.Gameplay.Movement
{
    public interface IMovementController
    {
        void MoveTo(Vector3 position);
        void HardSet(Vector3 position);
        void Stop();
    }
}