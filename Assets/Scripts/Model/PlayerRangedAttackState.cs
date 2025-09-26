using UnityEngine;

public class PlayerRangedAttackState : AnimatorState
{
    public PlayerRangedAttackState(Animator animator) : base(animator)
    {
        isAttacking = true;
        attackType = 2;
    }
}
