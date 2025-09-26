using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMapping : AnimationSync
{
    protected override void Awake()
    {
        base.Awake();
        var playerAudioList = new List<(string key, AudioModel value)>
        {
            ("Idle", new AudioModel(idleClip, PlayLoop)),
            ("Walk", new AudioModel(walkClip, PlayLoop)),
            ("Attack", new AudioModel(attackClip, PlayOnce)),
            ("Death", new AudioModel(deathClip, PlayOnce))
        };

        stateAudioMap0 = new Dictionary<string, (AudioClip, Func<AudioClip, AudioClip>)>()
        {
            { "Idle", (idleClip, PlayLoop) },
            { "Walk", (walkClip, PlayLoop) },
            { "Attack", (attackClip, PlayOnce) },
            { "Death", (deathClip, PlayOnce) },
        };
        stateAudioMap = new Dictionary<string, AudioModel>()
        {
            { "Idle", new AudioModel(idleClip, PlayLoop) },
            { "Walk", new AudioModel(walkClip, PlayLoop) },
            { "Attack", new AudioModel(attackClip, PlayOnce) },
            { "Death", new AudioModel(deathClip, PlayOnce) }
        };

        particleSfxMap0 = new Dictionary<string, (ParticleSystem, Func<ParticleSystem, ParticleSystem>)>()
        {
            { "Idle", (idleEffect, PlayParticle) },
            { "Walk", (walkEffect, PlayParticle) },
            { "Attack", (attackEffect, PlayParticle) },
            { "Death", (deathEffect, PlayParticle) }
        };
        particleSfxMap = new Dictionary<string, AudioModel>()
        {
            { "Idle", new AudioModel(idleEffect, PlayParticle) },
            { "Walk", new AudioModel(walkEffect, PlayParticle) },
            { "Attack", new AudioModel(attackEffect, PlayParticle) },
            { "Death", new AudioModel(deathEffect, PlayParticle) }
        };

        foreach (var item in playerAudioList)
        {
            particleSfxMap[item.key] = item.value;
            stateAudioMap[item.key] = item.value;
        }
    }
    protected override void Update()
    {
        base.Update();
        foreach (var kvp in stateAudioMap)
        {
            if (WasInState(kvp.Key, animatorComponent))
            {
                if (currentState != kvp.Key)
                {
                    currentState = kvp.Key;
                    kvp.Value.Invoke();
                }
                return;
            }
        }

        if (currentState != "")
        {
            currentState = "";
            Source.Stop();
        }
    }
}