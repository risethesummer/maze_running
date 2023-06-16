using System.Collections.Generic;
using MazeRunning.Gameplay.Maze.Info;
using MazeRunning.Gameplay.Movement;
using UnityEngine;

namespace MazeRunning.Gameplay.Active
{
    public class BotSillyController
    {
        private readonly int _minRandTime;
        private readonly int _maxRandTime;
        private readonly BotMovementController _botMovementController;
        private readonly Queue<Vector3> _wayPoints = new();

        public BotSillyController(BotMovementController botMovementController, int minRandTime, int maxRandTime)
        {
            _botMovementController = botMovementController;
            _minRandTime = minRandTime;
            _maxRandTime = maxRandTime;
            _botMovementController.OnReachDestination += UpdateWaypoint;
        }
        public void RandomSillyTimes(GenMazeInfo genMazeInfo, Vector3 finalTarget)
        {
            _wayPoints.Clear();
            var times = Random.Range(_minRandTime, _maxRandTime + 1);
            for (var i = 0; i < times; i++)
                _wayPoints.Enqueue(genMazeInfo.GetRandomPositionInMaze());
            _wayPoints.Enqueue(finalTarget);
            UpdateWaypoint();
        }
        public void UpdateWaypoint()
        {
            if (_wayPoints.Count <= 0)
                return;
            var wayPoint = _wayPoints.Dequeue();
            _botMovementController.MoveTo(wayPoint);
            Debug.Log($"New silly point: {wayPoint}");
        }
    }
}