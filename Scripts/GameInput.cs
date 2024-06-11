using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler onInteractAction;
    public event EventHandler onOperateAction;
    private PlayerController playController;

    private void Awake()
    {
        playController = new PlayerController();
        playController.Player.Enable();
        playController.Player.Interact.performed += Interact_Performed;
        playController.Player.Operate.performed += Operate_performed;
    }

    private void Operate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        onOperateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        onInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized(){
        Vector2 inputVector = playController.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        return direction;
    }
}
