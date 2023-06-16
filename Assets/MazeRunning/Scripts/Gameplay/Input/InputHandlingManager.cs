using MazeRunning.Gameplay.Active;
using MazeRunning.Gameplay.Camera;
using MazeRunning.Gameplay.Movement;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MazeRunning.Gameplay.Input
{
    public class InputHandlingManager : MazeRunningInputActions.ICharacterControllerActions
    {
        private readonly PlayerCameraManager _playerCameraManager;
        private readonly IMovementController _movementController;
        private readonly BaseActiveController _activeController;
        //Wait for done processing the previous input
        public InputHandlingManager(MazeRunningInputActions inputActions, 
            PlayerCameraManager playerCameraManager, IMovementController movementController, 
            BaseActiveController baseActiveController)
        {
            inputActions.Enable();
            inputActions.CharacterController.AddCallbacks(this);
            _playerCameraManager = playerCameraManager;
            _movementController = movementController;
            _activeController = baseActiveController;
        }

        public void OnChangeView(InputAction.CallbackContext context)
        {
            if (!context.performed || !_activeController.IsActive)
                return;
            _playerCameraManager.ApplyNextView();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if (!context.performed || !_activeController.IsActive)
                return;
            var mousePos = context.ReadValue<Vector2>();
            var worldPos = _playerCameraManager.GetWorldPointFromScreenPoint(mousePos);
            //Not click on a valid point on the scene
            if (!worldPos.HasValue)
                return;
            _movementController.MoveTo(worldPos.Value);
        }
    }
}
