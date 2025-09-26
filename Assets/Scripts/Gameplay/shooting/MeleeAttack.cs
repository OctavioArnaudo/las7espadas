using UnityEngine;

public class MeleeAttack : MonoController
{
    protected float lastAttackTime;
    public float attackRange = 5f;
    public float attackTime = 1.5f;
    public float damage = 10f;

    protected override void Update()
    {
        base.Update();
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
            Debug.Log("�Ataque al jugador! Da�o: " + damage);

            // Aqu� podr�as reducir la vida del jugador si tiene un script de salud
            // jugador.GetComponent<SaludJugador>()?.RecibirDa�o(da�o);
        }
    }
}