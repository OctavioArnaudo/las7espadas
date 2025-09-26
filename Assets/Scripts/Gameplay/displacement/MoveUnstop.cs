using UnityEngine;

public class MoveUnstop : MoveVertical
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (displacementInput.x != 0 || displacementInput.y != 0)
        {
            rb.linearVelocity = displacementInput * maxMoveSpeed;
            rb.AddForce(displacementInput * maxMoveSpeed);
        }
    }
}