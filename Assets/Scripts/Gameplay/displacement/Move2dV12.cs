using UnityEngine;

public class Move2dV12 : Move2dV1
{
    private float _horizontalInput;
    public float horizontalInput
    {
        get => _horizontalInput;
        set => _horizontalInput = value;
    }

    private float _moveSpeed;
    public float moveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
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

    public Move2dV12(
        float horizontalInput,
        float moveSpeed,
        Rigidbody2D rb, 
        Vector2 displacementInput,
        bool isMoving
        )
        : base(
            displacementInput,
            rb,
            isMoving
            )
    {
        this.horizontalInput = horizontalInput;
        this.moveSpeed = moveSpeed;
        this.rb = rb;
        this.isMoving = isMoving;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isMoving == false)
        {
                rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
                isMoving = true;
        }
    }
}
