using UnityEngine;

[
    RequireComponent(
        typeof(SpriteRenderer)
    )
]
public class BaseSpriteRenderer : BaseRigidbody2D
{
    [SerializeField] protected SpriteRenderer sr;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }
}