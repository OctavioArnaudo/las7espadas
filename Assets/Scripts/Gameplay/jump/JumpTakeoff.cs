using UnityEngine;

public class JumpTakeoff : JumpHit
{
    protected override void Update()
    {
        base.Update();
        if (isGrounded && jumpPressed)
        {
            rb.AddForceY(jumpSpeed, ForceMode2D.Impulse);
        }
    }
}