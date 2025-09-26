using UnityEngine;
using UnityEngine.AI;

[
    RequireComponent(
        typeof(NavMeshAgent)
    )
]
public class BaseNavMeshAgent : BaseMixer
{
    protected NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }
}
