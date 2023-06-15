using MazeRunning.SharedStructures.Signals;
using MazeRunning.Utils.Views;
using Zenject;

namespace MazeRunning.GameViews.LevelViews
{
    public class WaitForGeneratingMazeView : WaitingView
    {
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<DoneGeneratingMaze>(() => gameObject.SetActive(false));
        }
    }
}