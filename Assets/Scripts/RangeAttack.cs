using UnityEngine;

public class RangeAttack : ProjectileShoot
{
    public float visionRange = 10f;

    protected override void Update()
    {
        base.Update();
        if (targetDistance <= visionRange)
        {
            agent.ResetPath();
            AttackRange();
        }
    }

    public void AttackRange()
    {
        if (Time.time - lastAttackTime >= attackTime)
        {
            lastAttackTime = Time.time;
            Debug.Log("¡Ataque a distancia!");
            ShootProjectile();
        }
    }
}