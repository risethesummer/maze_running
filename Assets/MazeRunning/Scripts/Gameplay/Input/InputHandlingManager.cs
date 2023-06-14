using MazeRunning.Gameplay.Camera;
using MazeRunning.Gameplay.Movement;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MazeRunning.Gameplay.Input
{
    public class InputHandlingManager : MazeRunningInputActions.ICharacterControllerActions
    {
        private readonly PlayerCameraManager _playerCameraManager;
        private readonly PlayerMovementController _movementController;
        //Wait for done processing the previous input
        public InputHandlingManager(MazeRunningInputActions inputActions, 
            PlayerCameraManager playerCameraManager, PlayerMovementController movementController)
        {
            inputActions.Enable();
            inputActions.CharacterController.AddCallbacks(this);
            _playerCameraManager = playerCameraManager;
            _movementController = movementController;
        }

        public void OnChangeView(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            _playerCameraManager.ApplyNextView();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if (!context.performed)
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
