using UnityEngine;

public class Move2dV1 : MonoModular
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

    private bool _isMoving;
    public bool isMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    public Move2dV1(
        Vector2 displacementInput,
        Rigidbody2D rb,
        bool isMoving
        )
    {
        this.displacementInput = displacementInput;
        this.rb = rb;
        this.isMoving = isMoving;
    }

    public virtual void OnUpdate()
    {
        if (isMoving == true)
        {
            // 1. Verificar si hay input
            if (displacementInput.magnitude < 0.1f)
            {
                rb.linearVelocity = Vector2.zero;
                isMoving = false; // No hubo movimiento activo
            }
        }

        isMoving = true; // Hubo movimiento activo
    }

    // Método para detener el movimiento, útil al salir de un estado.
    public virtual void OnExit()
    {
        rb.linearVelocity = Vector2.zero;
    }
}