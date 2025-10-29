using UnityEngine;

[
    RequireComponent(
        typeof(Rigidbody2D)
    )
]
public class BaseRigidbody2D : BasePlayerInput
{
    [SerializeField] protected Rigidbody2D rb;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }
}