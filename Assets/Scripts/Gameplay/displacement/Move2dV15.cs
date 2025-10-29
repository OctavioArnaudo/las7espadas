using UnityEngine;

/// <summary>
/// In Parachute
/// </summary>
public class Move2dV15 : Move2dV1
{
    private float _horizontalInput;
    public float horizontalInput
    {
        get => _horizontalInput;
        set => _horizontalInput = value;
    }

    private float _jumpDeceleration;
    public float jumpDeceleration
    {
        get => _jumpDeceleration;
        set => _jumpDeceleration = value;
    }

    private float _moveAcceleration;
    public float moveAcceleration
    {
        get => _moveAcceleration;
        set => _moveAcceleration = value;
    }

    private float _verticalInput;
    public float verticalInput
    {
        get => _verticalInput;
        set => _verticalInput = value;
    }

    private Rigidbody2D _rb;
    public Rigidbody2D rb
    {
        get => _rb;
        set => _rb = value;
    }

    private bool _isMoving;
    public bool isMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    public Move2dV15(
        float horizontalInput,
        float jumpDeceleration,
        float moveAcceleration,
        float verticalInput,
        Rigidbody2D rb,
        Vector2 displacementInput,
        bool isMoving
    ) : base(
        displacementInput,
        rb,
        isMoving
        )
    {
        this.moveAcceleration = moveAcceleration;
        this.jumpDeceleration = jumpDeceleration;
        this.horizontalInput = horizontalInput;
        this.verticalInput = verticalInput;
        this.isMoving = isMoving;
        this.rb = rb;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isMoving == false)
        {
                if (verticalInput != 0)
                {
                    Vector2 movement = new Vector2(horizontalInput * moveAcceleration, rb.gravityScale * -jumpDeceleration);
                    rb.linearVelocity = movement;
                    isMoving = true; // Hubo movimiento activo
                }
        }

        isMoving = false;
    }

}
