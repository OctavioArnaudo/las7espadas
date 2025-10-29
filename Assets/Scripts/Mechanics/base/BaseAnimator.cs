using UnityEngine;

[
    RequireComponent(
        typeof(Animator)
    )
]
public class BaseAnimator : MonoComponent
{
    [SerializeField] protected Animator animComponent;
    protected override void Awake()
    {
        base.Awake();
        animComponent = GetComponent<Animator>();
    }
}