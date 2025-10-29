using UnityEngine;

public class JumpGrounded : VictoryZone
{
    [SerializeField] protected Vector2 groundCheck;
    [SerializeField] protected float groundCheckRadius = 0.2f;
    protected override void Update()
    {
        base.Update();
        groundCheck = c2D.bounds.center;
        groundCheck.y = c2D.bounds.min.y;
        isGrounded = Physics2D.OverlapCircle(groundCheck, groundCheckRadius, cf2D.layerMask);
    }
    public virtual void OnDrawGizmo()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck, groundCheckRadius);
    }
}