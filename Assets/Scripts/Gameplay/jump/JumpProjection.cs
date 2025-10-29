using UnityEngine;

public class JumpProjection : JumpPerform
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        isGrounded = false;

        var deltaPosition = rb.linearVelocity * Time.deltaTime;

        var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        var move = moveAlongGround * deltaPosition.x;

        PerformMovement(move, false);

        move = Vector2.up * deltaPosition.y;

        PerformMovement(move, true);
    }
}