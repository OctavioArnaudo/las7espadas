using UnityEngine;

public class Move2dV13 : Move2dV1
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

    public Move2dV13(
        Rigidbody2D rb,
        float moveSpeed,
        Vector2 displacementInput,
        bool isMoving
    ) : base(displacementInput, rb, isMoving)
    {
        this.displacementInput = displacementInput;
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.isMoving = isMoving;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isMoving == false)
        {
                // Priorizar eje dominante
                if (Mathf.Abs(displacementInput.x) > Mathf.Abs(displacementInput.y))
                {
                    displacementInput = new Vector2(displacementInput.x, 0);
                }
                else
                {
                    displacementInput = new Vector2(0, displacementInput.y);
                }

                // Aplicar movimiento si hay input
                if (displacementInput.x != 0 || displacementInput.y != 0)
                {
                    rb.MovePosition(rb.position + displacementInput * moveSpeed * Time.fixedDeltaTime);
                }
                isMoving = true;
        }
    }
}
