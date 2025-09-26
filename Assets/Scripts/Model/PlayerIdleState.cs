using UnityEngine;

public class PlayerIdleState : AnimatorState
{
    public PlayerIdleState(Animator animator, float currentSpeed) : base(animator)
    {
        isIdle = true;
        maxMoveSpeed = currentSpeed;
    }
}
