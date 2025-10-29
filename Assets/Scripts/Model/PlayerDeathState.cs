using System.Collections;
using UnityEngine;

public class PlayerDeathState : FSModel
{

    private bool _isDead;
    public bool isDead
    {
        get => _isDead = controller.isDead;
        set => _isDead = true;
    }

    public PlayerDeathState(Animator animator) : base(animator)
    {
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("Death", new AudioModel(controller.deathClip, PlayOnce)));

        stateAudioMap0.Add("Death", (controller.deathClip, PlayOnce));

        stateAudioMap.Add("Death", new AudioModel(controller.deathClip, PlayOnce));

        particleSfxList.Add(("Death", new ParticleModel(controller.deathEffect, PlayParticle)));

        particleSfxMap0.Add("Death", (controller.deathEffect, PlayParticle));

        particleSfxMap.Add("Death", new ParticleModel(controller.deathEffect, PlayParticle));

    }

    public virtual void TriggerDeathEffect()
    {
        TriggerSfxParticle("Death");
        TriggerAudioState("Death");
    }

    public override void RestartState(string stateId)
    {
        base.RestartState(stateId);
        if (stateId == "Death")
        {
            ChangeState(new PlayerDeathState(animComponent));
        }
    }

}
