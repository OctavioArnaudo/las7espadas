using UnityEngine;

public class InitTarget : InitPrefab
{
    public Transform targetPoint;
    public float targetDistance;

    protected override void Update()
    {
        base.Update();
        targetDistance = Vector3.Distance(transform.position, targetPoint.position);
    }
}
