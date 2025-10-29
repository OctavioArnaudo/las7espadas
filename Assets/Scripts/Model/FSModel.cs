using System;
using UnityEngine;

public class FSModel : ParticleModel
{
    private FSModel fsm;

    public FSModel(Animator animator, ParticleSystem particle = null, Func<ParticleSystem, ParticleSystem> actionParticle = null)
    : base(particle, actionParticle)
    {
        this.animComponent = animator;
    }

    public virtual void ChangeState(FSModel newState)
    {
        if (newState == null || newState == fsm) return;

        fsm?.OnExit();
        fsm = newState;
        fsm.OnEnter();
    }

    public virtual void RestartState(string stateId)
    {
    }

    public virtual void OnEnter() {
        AnimV1 anim = new AnimV1(animComponent);
        anim.SetAnimatorParameters(this);
    }
    public virtual void OnExit() {
    }
    public virtual void OnUpdate() {
    }
}