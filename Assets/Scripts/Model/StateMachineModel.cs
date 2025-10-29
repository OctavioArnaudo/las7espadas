using UnityEngine;

public abstract class StateMachineModel : StateMachineBehaviour
{
    protected Animator animComponent;
    protected AnimatorStateInfo animStateInfo;
    protected int animLayerIndex;

    protected AudioSource Source;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected MonoController controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animComponent = animator;
        animStateInfo = stateInfo;
        animLayerIndex = layerIndex;

        Source = animComponent.GetComponentInParent<AudioSource>();
        if (Source) return;
        rb = animComponent.GetComponentInParent<Rigidbody2D>();
        if (rb == null) return;
        sr = animComponent.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        controller = animComponent.GetComponentInParent<MonoController>();
        if (controller == null) return;
    }
}