using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionReleased : ActionPressed
{
    protected override void Update()
    {
        base.Update();
        try
        {
            jumpReleased = Input.GetButtonUp("Jump");
        }
        catch (Exception)
        {
            jumpReleased = jumpAction.WasReleasedThisFrame();
        }
    }

    public override void OnJump(InputAction.CallbackContext context)
    {
        base.OnJump(context);
        if (context.started || context.phase == InputActionPhase.Started)
        {
            jumpReleased = false;
            jumpPressed = true;
        }
        else if (context.performed)
        {
            jumpReleased = false;
            jumpPressed = true;
        }
        else if (context.canceled || context.phase == InputActionPhase.Canceled)
        {
            jumpPressed = false;
            jumpReleased = true;
        }
    }
    public override void OnJump(InputValue value)
    {
        base.OnJump(value);
        if (value.isPressed)
        {
            jumpReleased = false;
            jumpPressed = true;
        }
        else
        {
            jumpPressed = false;
            jumpReleased = true;
        }
    }
}