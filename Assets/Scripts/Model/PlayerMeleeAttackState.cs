using UnityEngine;

public class PlayerMeleeAttackState : AnimatorState
{
    public PlayerMeleeAttackState(Animator animator) : base(animator)
    {
        isAttacking = true;
        attackType = 1;
    }
}