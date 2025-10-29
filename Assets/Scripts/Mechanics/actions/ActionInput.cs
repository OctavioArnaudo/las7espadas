using UnityEngine;
using UnityEngine.InputSystem;

public class ActionInput : ActionDisable
{
    protected Vector2 displacementInput;
    protected Vector2 jumpInput;
    protected float scrollInput;

    protected override void Update()
    {
        base.Update();
        try
        {
            horizontalInput = Input.GetAxis(axisName: "Horizontal");
            displacementInput.x = horizontalInput;
            horizontalInput = Input.GetAxisRaw(axisName: "Horizontal");
            displacementInput.x = horizontalInput;
            verticalInput = Input.GetAxis(axisName: "Vertical");
            displacementInput.y = verticalInput;
            verticalInput = Input.GetAxisRaw(axisName: "Vertical");
            displacementInput.y = verticalInput;
            jumpInput.y = Input.GetAxis(axisName: "Jump");
            jumpInput.y = Input.GetAxisRaw(axisName: "Jump");
            scrollInput = Input.GetAxis(axisName: "Mouse ScrollWheel");
        }
        catch (System.Exception)
        {
            displacementInput = displacementAction.ReadValue<Vector2>();
            jumpInput.y = displacementInput.y;
        }
    }

    public virtual void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValue<Vector2>();
    }
    public virtual void OnJump(InputValue value)
    {
        jumpInput = value.Get<Vector2>();
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        displacementInput = context.ReadValue<Vector2>();
    }
    public virtual void OnMove(InputValue value)
    {
        displacementInput = value.Get<Vector2>();
    }
}