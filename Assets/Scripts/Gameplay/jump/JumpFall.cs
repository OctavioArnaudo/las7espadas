using UnityEngine;

public class JumpFall : JumpTakeoff
{
    protected override void Update()
    {
        base.Update();
        if (isJumping && jumpReleased)
        {
            isJumping = false;
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpDeceleration);
            }
        }
    }
}