using UnityEngine;

public class MoveDashingV2 : MoveDashingV1
{

    private bool _isTouchingWall;
    public bool isTouchingWall
    {
        get => _isTouchingWall;
        set => _isTouchingWall = value;
    }

    private bool _isWallSliding;
    public bool isWallSliding
    {
        get => _isWallSliding;
        set => _isWallSliding = value;
    }

    private int _wallDirX;
    public int wallDirX
    {
        get => _wallDirX;
        set => _wallDirX = value;
    }

    private float _wallSlideSpeed;
    public float wallSlideSpeed
    {
        get => _wallSlideSpeed;
        set => _wallSlideSpeed = value;
    }

    private LayerMask _layerMask;
    public LayerMask layerMask
    {
        get => _layerMask;
        set => _layerMask = value;
    }

    private bool _isGrounded;
    public bool isGrounded
    {
        get => _isGrounded;
        set => _isGrounded = value;
    }

    public MoveDashingV2(
        AudioClip dashClip,
        AudioSource source,
        bool canDash,
        bool isDashing,
        float dashCooldown,
        float dashDuration,
        float dashForce,
        float originalGravity,
        Rigidbody2D rb,
        Transform transform,
        Vector2 displacementInput
        ) : base(
            dashClip,
            source,
            canDash,
            isDashing,
            dashCooldown,
            dashDuration,
            dashForce,
            originalGravity,
            rb,
            transform,
            displacementInput
            )
    {
    }

    protected virtual void WallSlide()
    {
        DetectWallContact();       // Línea 1
        DetermineWallDirection();  // Línea 2
        EvaluateWallSliding();     // Línea 3
        ApplyWallSlideVelocity();  // Línea 4
    }
    protected virtual void DetectWallContact()
    {
        isTouchingWall = Physics2D.OverlapCircle(transform.position + Vector3.right * 0.5f, 0.1f, layerMask) ||
                         Physics2D.OverlapCircle(transform.position + Vector3.left * 0.5f, 0.1f, layerMask);
    }

    protected virtual void DetermineWallDirection()
    {
        wallDirX = Physics2D.OverlapCircle(transform.position + Vector3.right * 0.5f, 0.1f, layerMask) ? 1 :
                   Physics2D.OverlapCircle(transform.position + Vector3.left * 0.5f, 0.1f, layerMask) ? -1 : 0;
    }

    protected virtual void EvaluateWallSliding()
    {
        isWallSliding = !isGrounded && isTouchingWall && rb.linearVelocity.y < 0;
    }

    protected virtual void ApplyWallSlideVelocity()
    {
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }

}