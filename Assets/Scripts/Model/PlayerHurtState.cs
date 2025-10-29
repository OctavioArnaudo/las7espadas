using System.Collections;
using UnityEngine;

public class PlayerHurtState : FSModel
{

    private bool _isHurt;
    public bool isHurt
    {
        get => _isHurt = controller.isHurt;
        set => _isHurt = true;
    }

    private int _currentHP;
    public int currentHP
    {
        get => _currentHP = controller.currentHP;
        set => _currentHP = value;
    }

    public PlayerHurtState(Animator animator, int currentHealth) : base(animator)
    {
        currentHP = currentHealth;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("Hurt", new AudioModel(controller.hurtClip, PlayLoop)));

        stateAudioMap0.Add("Hurt", (controller.hurtClip, PlayLoop));

        stateAudioMap.Add("Hurt", new AudioModel(controller.hurtClip, PlayLoop));

        particleSfxList.Add(("Hurt", new ParticleModel(controller.hurtEffect, PlayParticle)));

        particleSfxMap0.Add("Hurt", (controller.hurtEffect, PlayParticle));

        particleSfxMap.Add("Hurt", new ParticleModel(controller.hurtEffect, PlayParticle));

    }

    public virtual void TriggerHurtEffect()
    {
        TriggerSfxParticle("Hurt");
        TriggerAudioState("Hurt");
    }

    public override void RestartState(string stateId)
    {
        base.RestartState(stateId);
        if (stateId == "Hurt")
        {
            ChangeState(new PlayerHurtState(animComponent, currentHP));
        }
    }

}
