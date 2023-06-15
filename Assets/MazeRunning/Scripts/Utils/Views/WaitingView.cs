using DG.Tweening;
using UnityEngine;

namespace MazeRunning.Utils.Views
{
    public class WaitingView : MonoBehaviour
    {
        [SerializeField] private float rotationInSecond;
        private void Start()
        {
            Wait();
        }

        private void Wait()
        {
            transform.DOLocalRotate(new Vector3(0, 0, 360), rotationInSecond, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetRelative(true)
                .SetEase(Ease.Linear);
        }
    }
}