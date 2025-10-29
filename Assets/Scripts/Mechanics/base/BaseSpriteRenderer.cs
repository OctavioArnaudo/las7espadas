using UnityEngine;

[
    RequireComponent(
        typeof(SpriteRenderer)
    )
]
public class BaseSpriteRenderer : BaseSlider
{
    [SerializeField] protected SpriteRenderer sr;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }
}