using System;
using MazeRunning.Sound;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Movement
{
    public class PlayerMovementController : IFixedTickable, IMovementController
    {
        private readonly RotationController _rotationController;
        private readonly CharacterController _cc;
        private readonly DelaySound _delaySound;
        private readonly float _minDistance;
        private readonly float _maxDistance;
        private readonly float _speed;
        private Vector3 _currentTarget;
        private Vector3 _currentOffset;
        private bool _donePath = true;
        private readonly RaycastHit[] _hits;
        private Vector3 SelfPosition => _cc.transform.position;
        public PlayerMovementController(CharacterController cc, RotationController rotationController, DelaySound delaySound, 
            float speed, float minDistance, float maxDistance)
        {
            _minDistance = minDistance;
            _maxDistance = maxDistance;
            _delaySound = delaySound;
            _speed = speed;
            _hits = new[]
            {
                new RaycastHit()
            };
            _cc = cc;
            _currentTarget = SelfPosition;
            _rotationController = rotationController;
        }
        public void MoveTo(Vector3 position)
        {
            var curPos = SelfPosition;
            position.y = curPos.y;
            var offset = (position - curPos).normalized;
            offset.y = 0;
            if (Physics.RaycastNonAlloc(curPos, offset, _hits, _maxDistance) > 0)
                _currentTarget = _hits[0].point;
            else
                _currentTarget = position;
            _currentOffset = offset;
            _donePath = false;
        }
        public void HardSet(Vector3 position)
        {
            _cc.transform.position = position;
        }

        public void Stop()
        {
            _currentTarget = SelfPosition;
        }

        public void FixedTick()
        {
            if (_donePath)
                return;
            if ((_currentTarget - SelfPosition).magnitude < _minDistance)
            {
                _donePath = true;
                return;
            }
            _rotationController.LookRotation(_currentOffset);
            _cc.Move(_currentOffset * _speed * Time.fixedDeltaTime);
            _delaySound.PlaySound(SoundName.Rolling);
        }
    }
}