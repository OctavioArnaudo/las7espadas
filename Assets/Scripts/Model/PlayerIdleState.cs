using UnityEngine;

public class PlayerIdleState : FSModel
{

    private bool _isIdle;
    public bool isIdle
    {
        get => _isIdle = controller.isIdle;
        set => _isIdle = true;
    }

    private float _moveSpeed;
    public float moveSpeed
    {
        get => _moveSpeed = controller.moveSpeed;
        set => _moveSpeed = value;
    }

    public PlayerIdleState(Animator animator, float movementSpeed) : base(animator)
    {
        moveSpeed = movementSpeed;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        stateAudioList.Add(("Idle", new AudioModel(controller.idleClip, PlayLoop)));

        stateAudioMap0.Add("Idle", (controller.idleClip, PlayLoop));

        stateAudioMap.Add("Idle", new AudioModel(controller.idleClip, PlayLoop));

        particleSfxList.Add(("Idle", new ParticleModel(controller.idleEffect, PlayParticle)));

        particleSfxMap0.Add("Idle", (controller.idleEffect, PlayParticle));

        particleSfxMap.Add("Idle", new ParticleModel(controller.idleEffect, PlayParticle));

    }

    public virtual void TriggerIdleEffect()
    {
        TriggerSfxParticle("Idle");
        TriggerAudioState("Idle");
    }

    public override void RestartState(string stateId)
    {
        base.RestartState(stateId);
        if (stateId == "Iddle") {
            ChangeState(new PlayerIdleState(animComponent, moveSpeed));
        }
    }

}
