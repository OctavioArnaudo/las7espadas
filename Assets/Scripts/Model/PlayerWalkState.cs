using UnityEngine;

public class PlayerWalkState : FSModel
{

    private bool _isWalking;
    public bool isWalking
    {
        get => _isWalking = controller.isWalking;
        set => _isWalking = true;
    }

    public PlayerWalkState(Animator animator, float currentSpeed) : base(animator)
    {
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("Walk", new AudioModel(controller.walkClip, PlayLoop)));

        stateAudioMap0.Add("Walk", (controller.walkClip, PlayLoop));

        stateAudioMap.Add("Walk", new AudioModel(controller.walkClip, PlayLoop));

        particleSfxList.Add(("Walk", new ParticleModel(controller.walkEffect, PlayParticle)));

        particleSfxMap0.Add("Walk", (controller.walkEffect, PlayParticle));

        particleSfxMap.Add("Walk", new ParticleModel(controller.walkEffect, PlayParticle));

    }

    public virtual void TriggerWalkEffect()
    {
        TriggerSfxParticle("Walk");
        TriggerAudioState("Walk");
    }

}
