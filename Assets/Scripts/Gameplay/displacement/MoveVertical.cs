using UnityEngine;

public class MoveVertical : MonoController
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Mathf.Abs(displacementInput.x) > Mathf.Abs(displacementInput.y))
        {
            displacementInput = new Vector2(displacementInput.x, 0);
        }
        else
        {
            displacementInput = new Vector2(0, displacementInput.y);
        }
        if (displacementInput.x != 0 || displacementInput.y != 0)
        {
            rb.MovePosition(rb.position + displacementInput * maxMoveSpeed * Time.fixedDeltaTime);
        }
    }
}