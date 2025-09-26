using UnityEngine;

public interface AnimatorInterface
{
    void OnEnter();
    void OnExit();
    void OnUpdate();
    void ApplyToAnimator(Animator animator);
}