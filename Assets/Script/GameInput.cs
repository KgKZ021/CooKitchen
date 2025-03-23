using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); //Player is the action Map we created.need to enable manually

        playerInputActions.Player.Interact.performed += Interact_performed; //in VS , Tab will auto create function for event
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
     
        //Happens when E is pressed.
        //always need to check or null reference exception can happen if there are no listeners or subscribers
        //if (OnInteractAction != null)
        //{
        //    OnInteractAction(this, EventArgs.Empty);
        //}
        OnInteractAction?.Invoke(this,EventArgs.Empty); //same operation
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); //read vector 2 value

        inputVector = inputVector.normalized; //not to increease thee magnitude

        return inputVector;
    }
}
