using UnityEngine;

public class ProjectileShoot : MonoModular
{
    private float _spawnSpeed;
    public float spawnSpeed
    {
        get => _spawnSpeed;
        set => _spawnSpeed = value;
    }

    private Transform _transform;
    public Transform transform
    {
        get => _transform;
        set => _transform = value;
    }

    public ProjectileShoot(
        Transform transform,
        float spawnSpeed
        )
    {
        this.spawnSpeed = spawnSpeed;
    }
    /*
    public virtual void Start()
    {
        OnObjectSpawned += ShootProjectile;
    }
    */
    public virtual void OnUpdate(GameObject instance)
    {
        Vector3 direction = (instance.transform.position - transform.position).normalized;
        float speed = Random.Range(1f, spawnSpeed);

        Rigidbody rb = instance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.transform.position = new Vector3(instance.transform.position.x + 1f * Time.deltaTime, instance.transform.position.y, instance.transform.position.z);
            rb.linearVelocity = direction * speed;
        }
    }
}