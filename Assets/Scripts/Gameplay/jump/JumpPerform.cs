using UnityEngine;

public class JumpPerform : MonoController
{
    [Header("Ground Detection")]
    [Tooltip("The minimum y-component of a surface's normal vector to be considered ground.")]
    protected float minGroundNormalY = .65f;
    [Tooltip("The distance of the collision detection shell.")]
    protected float shellRadius = 0.01f;

    protected const float minMoveDistance = 0.001f;

    protected Vector2 groundNormal;

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    protected virtual void PerformMovement(Vector2 move, bool yMovement)
    {
        var distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            //check if we hit anything in current direction of travel
            var count = rb.Cast(move, cf2D, hitBuffer, distance + shellRadius);
            for (var i = 0; i < count; i++)
            {
                var currentNormal = hitBuffer[i].normal;

                //is this surface flat enough to land on?
                if (currentNormal.y > minGroundNormalY)
                {
                    isGrounded = true;
                    // if moving up, change the groundNormal to new surface normal.
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                if (isGrounded)
                {
                    //how much of our velocity aligns with surface normal?
                    var projection = Vector2.Dot(rb.linearVelocity, currentNormal);
                    if (projection < 0)
                    {
                        //slower velocity if moving against the normal (up a hill).
                        displacementInput = rb.linearVelocity - projection * currentNormal;
                    }
                }
                else
                {
                    //We are airborne, but hit something, so cancel vertical up and horizontal velocity.
                    displacementInput.x *= 0;
                    displacementInput.y = Mathf.Min(rb.linearVelocity.y, 0);
                }
                //remove shellDistance from actual move distance.
                var modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rb.position = rb.position + move.normalized * distance;
    }
}