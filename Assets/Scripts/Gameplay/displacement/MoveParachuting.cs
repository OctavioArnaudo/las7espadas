using UnityEngine;

public class MoveParachuting : MonoController
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (displacementInput.y != 0)
        {
            Vector2 movement = new Vector2(displacementInput.x * moveAcceleration, rb.gravityScale * -jumpDeceleration);
            rb.linearVelocity = movement;
        }
    }
}