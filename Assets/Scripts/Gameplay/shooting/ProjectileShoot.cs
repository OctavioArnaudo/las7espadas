using UnityEngine;

public class ProjectileShoot : MeleeAttack
{
    public float projectileSpeed = 10f;

    public void ShootProjectile(GameObject projectile = null, Vector3? origin = null, Transform target = null, float? maxSpeed = null)
    {
        GameObject finalProjectile = projectile ?? projectilePrefab;
        Vector3 finalOrigin = origin ?? transform.position;
        Transform finalTarget = target ?? targetPoint;
        float finalSpeed = maxSpeed ?? projectileSpeed;

        GameObject projectileInstance = Object.Instantiate(finalProjectile, finalOrigin, Quaternion.identity);

        Vector3 direction = (finalTarget.position - finalOrigin).normalized;
        float speed = Random.Range(1f, finalSpeed);

        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.transform.position = new Vector3(finalOrigin.x + 1f * Time.deltaTime, finalOrigin.y, finalOrigin.z);
            rb.linearVelocity = direction * speed;
        }
    }
}