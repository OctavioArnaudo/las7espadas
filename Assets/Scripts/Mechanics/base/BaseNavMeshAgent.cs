using UnityEngine;
using UnityEngine.AI;

[
    RequireComponent(
        typeof(NavMeshAgent)
    )
]
public class BaseNavMeshAgent : BaseMixer
{
    [SerializeField] protected NavMeshAgent agent;

    /// <summary>
    /// 
    /// The PatrolPath defines the path that the enemy will follow.
    /// 
    /// </summary>
    [SerializeField] protected PatrolPath path;

    /// <summary>
    /// 
    /// The mover is used to control the enemy's movement along the patrol path.
    /// 
    /// </summary>
    [SerializeField] protected internal PatrolPath.Mover mover;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }
}
