using UnityEngine;

public class PlayerMeleeAttackState : FSModel
{

    private bool _isAttacking;
    public bool isAttacking
    {
        get => _isAttacking = controller.isAttacking;
        set => _isAttacking = true;
    }

    private int _attackType;
    public int attackType
    {
        get => _attackType = controller.attackType;
        set => _attackType = 1;
    }

    public PlayerMeleeAttackState(Animator animator) : base(animator)
    {
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("MeleeAttack", new AudioModel(controller.meleeAttackClip, PlayOnce)));

        stateAudioMap0.Add("MeleeAttack", (controller.meleeAttackClip, PlayOnce));

        stateAudioMap.Add("MeleeAttack", new AudioModel(controller.meleeAttackClip, PlayOnce));

        particleSfxList.Add(("MeleeAttack", new ParticleModel(controller.meleeAttackEffect, PlayParticle)));

        particleSfxMap0.Add("MeleeAttack", (controller.meleeAttackEffect, PlayParticle));

        particleSfxMap.Add("MeleeAttack", new ParticleModel(controller.meleeAttackEffect, PlayParticle));

    }

    public virtual void TriggerMeleeAttackEffect()
    {
        TriggerSfxParticle("MeleeAttack");
        TriggerAudioState("MeleeAttack");
    }

    public override void RestartState(string stateId)
    {
        base.RestartState(stateId);
        if (stateId == "MeleeAttack")
        {
            ChangeState(new PlayerMeleeAttackState(animComponent));
        }
    }

}