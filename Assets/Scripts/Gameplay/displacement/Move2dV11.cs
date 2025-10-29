using UnityEngine;

public class Move2dV11 : Move2dV1
{

    private Vector2 _displacementInput;
    public Vector2 displacementInput
    {
        get => _displacementInput;
        set => _displacementInput = value;
    }

    private Rigidbody2D _rb;
    public Rigidbody2D rb
    {
        get => _rb;
        set => _rb = value;
    }

    private float _moveSpeed;
    public float moveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    private bool _isMoving;
    public bool isMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    public Move2dV11(Rigidbody2D rb,
                         float moveSpeed,
                         Vector2 displacementInput,
                         bool isMoving) : base(displacementInput,
                                               rb,
                                               isMoving)
    {
        this.displacementInput = displacementInput;
        this.moveSpeed = moveSpeed;
        this.rb = rb;
        this.isMoving = isMoving;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isMoving == false)
        {
                // 2. Aplicar Física
                Vector2 velocity = displacementInput.normalized * moveSpeed;
                rb.linearVelocity = velocity;
                isMoving = true; // Hubo movimiento activo
        }
    }

}