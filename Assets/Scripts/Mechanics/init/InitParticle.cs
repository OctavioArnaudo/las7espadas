using System;
using System.Collections.Generic;
using UnityEngine;

public class InitParticle : InitPanel
{
    [Header("Particle Systems")]
    [SerializeField] public ParticleSystem attackEffect;
    [SerializeField] public ParticleSystem dashEffect;
    [SerializeField] public ParticleSystem deathEffect;
    [SerializeField] public ParticleSystem idleEffect;
    [SerializeField] public ParticleSystem jumpEffect;
    [SerializeField] public ParticleSystem landEffect;
    [SerializeField] public ParticleSystem respawnEffect;
    [SerializeField] public ParticleSystem victoryEffect;
    [SerializeField] public ParticleSystem walkEffect;

    [SerializeField] protected Dictionary<string, (ParticleSystem, Func<ParticleSystem, ParticleSystem>)> particleSfxMap0;
    [SerializeField] protected Dictionary<string, AudioModel> particleSfxMap;

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

    public virtual void TriggerIdleEffect()
    {
    }
    public virtual void TriggerWalkEffect()
    {
    }
    public virtual void TriggerAttackEffect()
    {
    }
    public virtual void TriggerDashEffect()
    {
    }
    public virtual void TriggerDeathEffect()
    {
    }
    public virtual void TriggerJumpEffect()
    {
    }
    public virtual void TriggerLandEffect()
    {
    }
    public virtual void TriggerRespawnEffect()
    {
    }
    public virtual void TriggerVictoryEffect()
    {
    }
}