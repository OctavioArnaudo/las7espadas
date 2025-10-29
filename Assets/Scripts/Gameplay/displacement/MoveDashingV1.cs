using UnityEngine;

public class MoveDashingV1 : MonoModular
{
    private AudioClip _dashClip;
    public AudioClip dashClip
    {
        get => _dashClip;
        set => _dashClip = value;
    }

    private AudioSource _source;
    public AudioSource Source
    {
        get => _source;
        set => _source = value;
    }

    private bool _canDash;
    public bool canDash
    {
        get => _canDash;
        set => _canDash = value;
    }

    private bool _isDashing;
    public bool isDashing
    {
        get => _isDashing;
        set => _isDashing = value;
    }

    private float _dashCooldown;
    public float dashCooldown
    {
        get => _dashCooldown;
        set => _dashCooldown = value;
    }

    private float _dashDuration;
    public float dashDuration
    {
        get => _dashDuration;
        set => _dashDuration = value;
    }

    private float _dashForce;
    public float dashForce
    {
        get => _dashForce;
        set => _dashForce = value;
    }

    private float _originalGravity;
    public float originalGravity
    {
        get => _originalGravity;
        set => _originalGravity = value;
    }

    private Rigidbody2D _rb;
    public Rigidbody2D rb
    {
        get => _rb;
        set => _rb = value;
    }

    private Transform _transform;
    public Transform transform
    {
        get => _transform;
        set => _transform = value;
    }

    private Vector2 _displacementInput;
    public Vector2 displacementInput
    {
        get => _displacementInput;
        set => _displacementInput = value;
    }

    public MoveDashingV1(
     AudioClip dashClip,
     AudioSource Source,
     bool canDash,
     bool isDashing,
     float dashCooldown,
     float dashDuration,
     float dashForce,
     float originalGravity,
     Rigidbody2D rb,
     Transform transform,
     Vector2 displacementInput
        )
    {
        this.dashClip = dashClip;
        this.Source = Source;
        this.canDash = canDash;
        this.isDashing = isDashing;
        this.dashCooldown = dashCooldown;
        this.rb = rb;
        this.dashForce = dashForce;
        this.originalGravity = originalGravity;
        this.displacementInput = displacementInput;
        this.transform = transform;
        this.dashDuration = dashDuration;
    }

    protected virtual void Dash()
    {
        DashStart();                     // Línea 1
        DashSetGravity();                // Línea 2
        DashApplyForce();                // Línea 3
        DashPlaySound();                 // Línea 4
        DashDuration();                  // Línea 5
        DashCooldown();                  // Línea 6
    }

    protected virtual void DashStart()
    {
        canDash = false;
        isDashing = true;
    }

    protected virtual void DashSetGravity()
    {
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    protected virtual void DashApplyForce()
    {
        float direction = (displacementInput.x != 0 ? Mathf.Sign(displacementInput.x) : transform.localScale.x);
        rb.linearVelocity = new Vector2(direction * dashForce, 0f);
    }

    protected virtual void DashPlaySound()
    {
        if (dashClip != null && Source != null)
            Source.PlayOneShot(dashClip);
    }

    protected virtual void DashDuration()
    {
        TimerDelayV1 timerDelay = new TimerDelayV1(dashDuration);
        timerDelay.OnStart();
        timerDelay.OnExit();
        if (timerDelay.isDone)
        {
            rb.gravityScale = originalGravity;
            isDashing = false;
        }
    }

    protected virtual void DashCooldown()
    {
        TimerDelayV1 timerDelay = new TimerDelayV1(dashCooldown);
        timerDelay.OnStart();
        timerDelay.OnExit();
        if (timerDelay.isDone)
        {
            canDash = true;
        }
    }


}