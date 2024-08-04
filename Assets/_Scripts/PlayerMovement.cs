using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerParams playerParams;
    public Rigidbody rb;
    private InputAction moveAction;

    private void Awake() {
        
        moveAction = GameManager.Instance.inputActions.Player.Move;
    }


    private void FixedUpdate() {
        Vector2 movementVector = moveAction.ReadValue<Vector2>().normalized;
        rb.linearVelocity = playerParams.movementSpeed * new Vector3 (movementVector.x,rb.linearVelocity.y,movementVector.y);
    }


}
