using UnityEngine;

/// <summary>
/// AnimationController integrates physics and animation. It is generally used for simple enemy animation.
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class AnimationController : KinematicObject
{
    /// <summary>
    /// Max horizontal speed.
    /// </summary>
    public float maxSpeed = 7;
    /// <summary>
    /// Max jump velocity
    /// </summary>
    public float jumpTakeOffSpeed = 7;

    /// <summary>
    /// Used to indicated desired direction of travel.
    /// </summary>
    public Vector2 move;

    /// <summary>
    /// Set to true to initiate a jump.
    /// </summary>
    public bool jump;

    /// <summary>
    /// Set to true to set the current jump velocity to zero.
    /// </summary>
    public bool stopJump;

    /// <summary>
    /// A global jump modifier applied to all initial jump velocities.
    /// </summary>
    public float jumpModifier = 1.5f;

    /// <summary>
    /// A global jump modifier applied to slow down an active jump when 
    /// the user releases the jump input.
    /// </summary>
    public float jumpDeceleration = 0.5f;

    SpriteRenderer spriteRenderer;
    Animator animator;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        if (jump && isGrounded)
        {
            velocity.y = jumpTakeOffSpeed * jumpModifier;
            jump = false;
        }
        else if (stopJump)
        {
            stopJump = false;
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * jumpDeceleration;
            }
        }

        if (move.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (move.x < -0.01f)
            spriteRenderer.flipX = true;

        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}