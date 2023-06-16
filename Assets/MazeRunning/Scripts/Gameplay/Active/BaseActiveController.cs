using MazeRunning.Gameplay.Movement;
using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using Zenject;

namespace MazeRunning.Gameplay.Active
{
    public class BaseActiveController
    {
        public bool IsActive { get; private set; }
        protected readonly IMovementController MovementController;
        private readonly PlayerType _playerType;
        public BaseActiveController(SignalBus signalBus, IMovementController movementController, PlayerType playerType)
        {
            MovementController = movementController;
            _playerType = playerType;
            signalBus.Subscribe<DoneGeneratingMaze>(SetAfterDoneGenerating);
            signalBus.Subscribe<EndLevelSignal>(SetAfterEndingLevel);
        }
        
        protected virtual void SetAfterEndingLevel(EndLevelSignal endLevelSignal)
        {
            IsActive = false;
        }

        protected virtual void SetAfterDoneGenerating(DoneGeneratingMaze d)
        {
            MovementController.HardSet(d.PlayerPositions[_playerType]);
            IsActive = true;
        }
    }
}