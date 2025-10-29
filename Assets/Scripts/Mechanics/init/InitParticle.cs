using System;
using UnityEngine;

[Serializable]
public class InitParticle : InitMixer
{
    [Header("Particle Systems")]
    public ParticleSystem dashEffect;
    public ParticleSystem deathEffect;
    public ParticleSystem healEffect;
    public ParticleSystem hurtEffect;
    public ParticleSystem idleEffect;
    public ParticleSystem jumpEffect;
    public ParticleSystem landEffect;
    public ParticleSystem meleeAttackEffect;
    public ParticleSystem platformBreakEffect;
    public ParticleSystem rangedAttackEffect;
    public ParticleSystem respawnEffect;
    public ParticleSystem victoryEffect;
    public ParticleSystem walkEffect;
}