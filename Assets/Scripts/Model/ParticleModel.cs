using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParticleModel : AudioModel
{
    public ParticleSystem Particle;
    public Func<ParticleSystem, ParticleSystem> ParticleAction;

    /// <summary>
    /// 
    /// Called when the state machine enters the state.
    /// 
    /// </summary>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        foreach (var item in particleSfxList)
        {
            particleSfxMap[item.key] = item.value;
        }
    }

    public ParticleModel(ParticleSystem particle, Func<ParticleSystem, ParticleSystem> actionParticle, AudioClip clip = null, Func<AudioClip, AudioClip> actionClip = null) : base(clip, actionClip)
    {
        Particle = particle;
        ParticleAction = actionParticle;
    }

    public override void Invoke()
    {
        base.Invoke();
        ParticleAction?.Invoke(Particle);
    }

    protected Dictionary<string, (ParticleSystem Particle, Func<ParticleSystem, ParticleSystem> Action)> particleSfxMap0;
    protected Dictionary<string, ParticleModel> particleSfxMap;
    protected List<(string key, ParticleModel value)> particleSfxList;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public ParticleSystem PlayParticle(ParticleSystem particle)
    {
        if (particle == null)
        {
            Debug.LogWarning("ParticleSystem is null. Please provide a valid ParticleSystem.");
            return null;
        }
        particle.Play();
        return particle;
    }

    public IEnumerator TriggerSfxParticle(string stateName)
    {
        if (particleSfxMap.TryGetValue(stateName, out var audioState))
        {
            audioState.Invoke();
        }
        yield return null;
    }

}