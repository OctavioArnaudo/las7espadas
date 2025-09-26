using UnityEngine;

public class PlayerDeathState : AnimatorState
{
    public PlayerDeathState(Animator animator) : base(animator)
    {
        isDead = true;
    }
}
