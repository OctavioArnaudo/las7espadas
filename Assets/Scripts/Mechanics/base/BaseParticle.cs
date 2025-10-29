using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BaseParticle : BaseNavMeshAgent
{

#if UNITY_TEMPLATE_PLATFORMER

    ParticleSystem p;

    protected override void Start()
    {
        base.Start();

        p = GetComponent<ParticleSystem>();

    }

#endif

}
