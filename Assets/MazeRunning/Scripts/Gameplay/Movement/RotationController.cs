using UnityEngine;

namespace MazeRunning.Gameplay.Movement
{
    public class RotationController
    {
        private readonly Transform _transform;
        private readonly float _turnSpeed;
        private readonly float _rotateSpeed;
        public RotationController(Transform transform, float turnSpeed, float rotateSpeed)
        {
            _transform = transform;
            _rotateSpeed = rotateSpeed;
            _turnSpeed = turnSpeed;
        }

        public void LookRotation(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, _turnSpeed);
            _transform.RotateAround(_transform.position, _transform.right,  _rotateSpeed);
        }
    }
}