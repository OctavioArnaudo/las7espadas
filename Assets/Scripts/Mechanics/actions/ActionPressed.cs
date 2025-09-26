using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionPressed : ActionInput
{
    protected override void Update()
    {
        base.Update();
        try
        {
            hurtPressed = Input.GetKeyDown(KeyCode.H);
            hurtPressed = Keyboard.current.hKey.wasPressedThisFrame;
            jumpPressed = Input.GetButtonDown("Jump");
            jumpPressed = Input.GetKeyDown(KeyCode.Space);
            jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
            spawnPressed = Input.GetKeyDown(KeyCode.S);
            spawnPressed = Keyboard.current.sKey.wasPressedThisFrame;
        }
        catch (Exception)
        {
            hurtPressed = hurtAction.WasPressedThisFrame();
            jumpPressed = jumpAction.WasPressedThisFrame();
            spawnPressed = spawnAction.WasPressedThisFrame();
        }
    }
}