using UnityEngine;

public class PlayerRangedAttackState : FSModel
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
        set => _attackType = 2;
    }

    public PlayerRangedAttackState(Animator animator) : base(animator)
    {
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("RangedAttack", new AudioModel(controller.rangedAttackClip, PlayOnce)));

        stateAudioMap0.Add("RangedAttack", (controller.rangedAttackClip, PlayOnce));

        stateAudioMap.Add("RangedAttack", new AudioModel(controller.rangedAttackClip, PlayOnce));

        particleSfxList.Add(("RangedAttack", new ParticleModel(controller.rangedAttackEffect, PlayParticle)));

        particleSfxMap0.Add("RangedAttack", (controller.rangedAttackEffect, PlayParticle));

        particleSfxMap.Add("RangedAttack", new ParticleModel(controller.rangedAttackEffect, PlayParticle));

    }

    public virtual void TriggerRangedAttackEffect()
    {
        TriggerSfxParticle("RangedAttack");
        TriggerAudioState("RangedAttack");
    }

    public override void RestartState(string stateId)
    {
        base.RestartState(stateId);
        if (stateId == "RangedAttack")
        {
            ChangeState(new PlayerRangedAttackState(animComponent));
        }
    }

}
