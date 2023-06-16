using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MazeRunning.Utils.Collections;
using MazeRunning.Utils.Task;
using UnityEngine;
using Zenject;

namespace MazeRunning.Gameplay.Camera
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCam;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private LinearTabCollection<Vector3> viewPoints;
        [SerializeField] private float moveDelta;
        private CancellationTokenTracker _cancellationTokenTracker;
        [Inject]
        public void Construct(CancellationTokenTracker cancellationTokenTracker)
        {
            _cancellationTokenTracker = cancellationTokenTracker;
        }
        public void ApplyNextView()
        {
            var newView = viewPoints.Tab();
            _cancellationTokenTracker.Reset();
            ApplyNextViewInternal(newView, _cancellationTokenTracker.CancellationToken).Forget();
        }
        private async UniTaskVoid ApplyNextViewInternal(Vector3 position, CancellationToken cancellationToken)
        {
            var curPos = mainCam.transform.localPosition;
            while (curPos != position && !cancellationToken.IsCancellationRequested)
            {
                mainCam.transform.localPosition = curPos = Vector3.MoveTowards(curPos, position, moveDelta);
                await UniTask.NextFrame(cancellationToken);
            }
        }
        public Vector3? GetWorldPointFromScreenPoint(Vector2 screenPoint)
        {
            var ray = mainCam.ScreenPointToRay(screenPoint);
            if (Physics.Raycast(ray, out var info, floorLayer))
                return info.point;
            return null;
        }
    }
}