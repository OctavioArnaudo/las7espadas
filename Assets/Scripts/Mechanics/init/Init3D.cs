using UnityEngine;

public class Init3D : Init2D
{
    // -----------------------------------------------------------
    // Métodos de Detección de Colisiones
    // -----------------------------------------------------------

    /// <summary>
    /// Se llama en el primer fotograma en que el Collider del GameObject entra en contacto con otro Collider.
    /// </summary>
    /// <param name="collision">Información de la colisión.</param>
    public virtual void CollisionEnter(Collision collision)
    {
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }

    /// <summary>
    /// Se llama en cada fotograma en que el Collider del GameObject está en contacto con otro Collider.
    /// </summary>
    /// <param name="collision">Información de la colisión.</param>
    public virtual void CollisionStay(Collision collision)
    {
    }
    protected virtual void OnCollisionStay(Collision collision)
    {
        CollisionStay(collision);
    }

    /// <summary>
    /// Se llama en el último fotograma en que el Collider del GameObject estaba en contacto con otro Collider.
    /// </summary>
    /// <param name="collision">Información de la colisión.</param>
    public virtual void CollisionExit(Collision collision)
    {
    }
    protected virtual void OnCollisionExit(Collision collision)
    {
        CollisionExit(collision);
    }

    public virtual void TriggerEnter(Collider collider)
    {
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        TriggerEnter(collider);
    }

    public virtual void TriggerStay(Collider collider)
    {
    }
    protected virtual void OnTriggerStay(Collider collider)
    {
        TriggerStay(collider);
    }

    public virtual void TriggerExit(Collider collider)
    {
    }
    protected virtual void OnTriggerExit(Collider collider)
    {
        TriggerExit(collider);
    }

}
