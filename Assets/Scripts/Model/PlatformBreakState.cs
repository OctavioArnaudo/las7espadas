using System.Collections;
using UnityEngine;

public class PlatformBreakState : FSModel
{
    protected bool isBroken;

    public PlatformBreakState(Animator animator) : base(animator)
    {
        isBroken = controller.isBroken;
        isBroken = true;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("PlatformBreak", new AudioModel(controller.platformBreakClip, PlayOnce)));

        stateAudioMap0.Add("PlatformBreak", (controller.platformBreakClip, PlayOnce));

        stateAudioMap.Add("PlatformBreak", new AudioModel(controller.platformBreakClip, PlayOnce));

        particleSfxList.Add(("PlatformBreak", new ParticleModel(controller.platformBreakEffect, PlayParticle)));

        particleSfxMap0.Add("PlatformBreak", (controller.platformBreakEffect, PlayParticle));

        particleSfxMap.Add("PlatformBreak", new ParticleModel(controller.platformBreakEffect, PlayParticle));

    }

    public virtual IEnumerator TriggerPlatformBreakEffect()
    {
        TriggerSfxParticle("PlatformBreak");
        TriggerAudioState("PlatformBreak");
        yield return null;
    }

}