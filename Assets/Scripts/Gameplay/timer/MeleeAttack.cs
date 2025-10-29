using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoModular
{
    private float _lastAttackTime;
    public float lastAttackTime
    {
        get => _lastAttackTime;
        set => _lastAttackTime = value;
    }

    private float _attackRange;
    public float attackRange
    {
        get => _attackRange;
        set => _attackRange = value;
    }

    private float _attackTime;
    public float attackTime
    {
        get => _attackTime;
        set => _attackTime = value;
    }

    private float _damage;
    public float damage
    {
        get => _damage;
        set => _damage = value;
    }

    private NavMeshAgent _agent;
    public NavMeshAgent agent
    {
        get => _agent;
        set => _agent = value;
    }

    private float _spawnDistance;
    public float spawnDistance
    {
        get => _spawnDistance;
        set => _spawnDistance = value;
    }

    public MeleeAttack(
        float lastAttackTime,
        float attackRange,
        float attackTime,
        float damage,
        float spawnDistance,
        UnityEngine.AI.NavMeshAgent agent
        )
    {
        this.lastAttackTime = lastAttackTime;
        this.attackRange = attackRange;
        this.attackTime = attackTime;
        this.damage = damage;
    }

    public virtual void OnUpdate()
    {
        if (spawnDistance <= attackRange)
        {
            agent.ResetPath();
            if (Time.time - lastAttackTime >= attackTime)
            {
                lastAttackTime = Time.time;
                Debug.Log("�Ataque al jugador! Da�o: " + damage);
            }
        }
    }
}