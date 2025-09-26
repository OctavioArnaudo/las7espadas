using UnityEngine;

[
    RequireComponent(
        typeof(Collider2D)
    )
]
public class BaseCollider2D : BaseAudioSource
{
    /*internal new*/
    [SerializeField] protected Collider2D c2D;

    protected override void Awake()
    {
        base.Awake();
        c2D = GetComponent<Collider2D>();
    }
}