using MazeRunning.SharedStructures.Signals;
using UnityEngine;
using Zenject;

namespace MazeRunning.GameViews.LevelViews
{
    public class WaitForGeneratingMazeView : MonoBehaviour
    {
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<DoneGeneratingMaze>(() => gameObject.SetActive(false));
        }
    }
}