using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementControl
{
    public void MovementInput(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }
}
