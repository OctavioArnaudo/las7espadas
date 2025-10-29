using System.Collections;
using UnityEngine;

public class PlayerHealState : FSModel
{

    private float _currentHP;
    public float currentHP
    {
        get => _currentHP = controller.currentHP;
        set => _currentHP = value;
    }
    private bool _isAlive;
    public bool isAlive
    {
        get => _isAlive = controller.isAlive;
        set => _isAlive = true;
    }

    public PlayerHealState(Animator animator, float currentHealth) : base(animator)
    {
        currentHP = currentHealth;
    }

    /// <summary>
    /// Increment the HP of the entity.
    /// </summary>
    public void Increment()
    {
        // If the current HP is already at max, do nothing.
        currentHP = Mathf.Clamp(currentHP + 1, 0, controller.maxHP);
    }

    /// <summary>
    /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
    /// current HP reaches 0.
    /// </summary>
    public virtual void Decrement(int amount = 1)
    {
        // If the current HP is already at 0, do nothing.
        currentHP = Mathf.Clamp(currentHP - amount, 0, controller.maxHP);

        if (currentHP <= 0)
        {
            Destroy(controller.gameObject);
        }
        else
        {
            int index = Mathf.Clamp(controller.damageStates.Length - Mathf.FloatToHalf(currentHP), 0, controller.damageStates.Length - 1);
            sr.sprite = controller.damageStates[index];
        }
    }

    /// <summary>
    /// Decrement the HP of the entitiy until HP reaches 0.
    /// </summary>
    public void Die()
    {
        // If the current HP is already at 0, do nothing.
        while (isAlive) Decrement();
    }

    /// <summary>
    /// 
    /// Called when the script is first loaded or when the game object is instantiated.
    /// 
    /// </summary>
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Initialize the current HP to the maximum HP.
        currentHP = controller.maxHP;
        
        stateAudioList.Add(("Heal", new AudioModel(controller.healClip, PlayLoop)));

        stateAudioMap0.Add("Heal", (controller.healClip, PlayLoop));

        stateAudioMap.Add("Heal", new AudioModel(controller.healClip, PlayLoop));

        particleSfxList.Add(("Heal", new ParticleModel(controller.healEffect, PlayParticle)));

        particleSfxMap0.Add("Heal", (controller.healEffect, PlayParticle));

        particleSfxMap.Add("Heal", new ParticleModel(controller.healEffect, PlayParticle));

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        isAlive = currentHP > 0;
    }

    public virtual IEnumerator TriggerHurtEffect()
    {
        TriggerSfxParticle("Heal");
        TriggerAudioState("Heal");
        yield return null;
    }

}