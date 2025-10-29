using UnityEngine;

public class InitGizmos : InitCoroutine
{

    protected Vector2 raySize;

    protected override void Awake() {
        base.Awake();
        raySize = transform.TransformDirection(Vector2.down);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, raySize);
    }

}