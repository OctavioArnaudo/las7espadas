using UnityEngine;

public class RangeAttack : MonoModular
{
    private float _visionRange;
    public float visionRange
    {
        get => _visionRange;
        set => _visionRange = value;
    }

    private float _spawnDistance;
    public float spawnDistance
    {
        get => _spawnDistance;
        set => _spawnDistance = value;
    }

    private float _attackTime;
    public float attackTime
    {
        get => _attackTime;
        set => _attackTime = value;
    }

    private float _lastAttackTime;
    public float lastAttackTime
    {
        get => _lastAttackTime;
        set => _lastAttackTime = value;
    }

    private UnityEngine.AI.NavMeshAgent _agent;
    public UnityEngine.AI.NavMeshAgent agent
    {
        get => _agent;
        set => _agent = value;
    }

    private GameObject _projectilePGO;
    public GameObject projectilePGO
    {
        get => _projectilePGO;
        set => _projectilePGO = value;
    }

    public RangeAttack(
        float visionRange,
        float spawnDistance,
        float attackTime,
        float lastAttackTime,
        UnityEngine.AI.NavMeshAgent agent,
        GameObject projectilePGO
        )
    {
        this.visionRange = visionRange;
        this.spawnDistance = spawnDistance;
        this.attackTime = attackTime;
        this.lastAttackTime = lastAttackTime;
        this.agent = agent;
        this.projectilePGO = projectilePGO;
    }

    public virtual void OnUpdate()
    {
        if (spawnDistance <= visionRange)
        {
            agent.ResetPath();
            if (Time.time - lastAttackTime >= attackTime)
            {
                lastAttackTime = Time.time;
                Debug.Log("�Ataque a distancia!");
                //ShootProjectile(projectilePGO);
            }
        }
    }
}