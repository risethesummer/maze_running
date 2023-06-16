using MazeRunning.Gameplay.Camera;
using MazeRunning.Gameplay.Movement;
using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using Zenject;

namespace MazeRunning.Gameplay.Active
{
    public class BotActiveController : BaseActiveController
    {
        private readonly BotCameraController _cameraController;
        private readonly BotSillyController _sillyController;
        
        public BotActiveController(SignalBus signalBus, BotSillyController sillyController, 
            IMovementController movementController, BotCameraController botCameraController) 
            : base(signalBus, movementController, PlayerType.AI)
        {
            _cameraController = botCameraController;
            _sillyController = sillyController;
        }
        protected override void SetAfterEndingLevel(EndLevelSignal endLevelSignal)
        {
            base.SetAfterEndingLevel(endLevelSignal);
            if (endLevelSignal.Winner == PlayerType.AI)
                _cameraController.Prior(true);
            MovementController.Stop();
        }

        protected override void SetAfterDoneGenerating(DoneGeneratingMaze d)
        {
            base.SetAfterDoneGenerating(d);
            _sillyController.RandomSillyTimes(d.GenMazeInfo, d.DoorInfo.Position);
            _cameraController.Prior(false);
        }
    }
}