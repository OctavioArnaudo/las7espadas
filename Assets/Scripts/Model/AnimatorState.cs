using UnityEngine;

public class AnimatorState : MonoController, AnimatorInterface
{
    public AnimatorState(Animator animator)
    {
        this.animatorComponent = animator;
    }

    public virtual void OnEnter() {
        SetAnimatorParameters(this, animatorComponent);
    }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }

    public virtual void ApplyToAnimator(Animator animator)
    {
        SetAnimatorParameters(this, animator);
    }
}