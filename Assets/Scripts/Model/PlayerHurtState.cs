using UnityEngine;

public class PlayerHurtState : AnimatorState
{
    public PlayerHurtState(Animator animator, int currentHealth) : base(animator)
    {
        isHurt = true;
        currentHP = currentHealth;
    }
}
