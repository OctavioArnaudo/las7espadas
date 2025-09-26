using UnityEngine;

[
    RequireComponent(
        typeof(Animator)
    )
]
public class BaseAnimator : MonoComponent
{
    [SerializeField] protected Animator animatorComponent;
    protected override void Awake()
    {
        base.Awake();
        animatorComponent = GetComponent<Animator>();
    }
}