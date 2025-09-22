using UnityEngine;
using UnityEngine.AI;

[
    RequireComponent(
        typeof(NavMeshAgent)
    )
]
public class MeleeAttack : MonoBehaviour
{
    protected NavMeshAgent agent;

    protected float lastAttackTime;
    public float attackRange = 5f;
    public float attackTime = 1.5f;
    public float damage = 10f;

    public Transform targetPoint;
    public float targetDistance;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        targetDistance = Vector3.Distance(transform.position, targetPoint.position);
        if (targetDistance <= attackRange)
        {
            agent.ResetPath();
            AttackMelee();
        }
    }

    protected void AttackMelee()
    {
        if (Time.time - lastAttackTime >= attackTime)
        {
            lastAttackTime = Time.time;
            Debug.Log("¡Ataque al jugador! Daño: " + damage);

            // Aquí podrías reducir la vida del jugador si tiene un script de salud
            // jugador.GetComponent<SaludJugador>()?.RecibirDaño(daño);
        }
    }
}