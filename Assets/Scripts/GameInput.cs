using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : Singleton<GameInput>
{
   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlternateAction;
   public event EventHandler OnPauseAction;
   private PlayerInputActions playerInputActions;

   private new void Awake()
   {
      base.Awake();
      playerInputActions = new();
      playerInputActions.Player.Enable();

      playerInputActions.Player.Interact.performed += Interact_performed;
      playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
      playerInputActions.Player.Pause.performed += Pause_performed;
   }

   private void OnDestroy()
   {
      playerInputActions.Player.Interact.performed -= Interact_performed;
      playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
      playerInputActions.Player.Pause.performed -= Pause_performed;

      playerInputActions.Dispose();
   }

   private void Pause_performed(InputAction.CallbackContext ctx)
   {
      OnPauseAction?.Invoke(this, EventArgs.Empty);
   }

   private void Interact_performed(InputAction.CallbackContext ctx)
   {
      OnInteractAction?.Invoke(this, EventArgs.Empty);
   }

   private void InteractAlternate_performed(InputAction.CallbackContext ctx)
   {
      OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
   }

   public Vector2 GetMovementVectorNormalized()
   {
      Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

      inputVector.Normalize();

      return inputVector;
   }
}
