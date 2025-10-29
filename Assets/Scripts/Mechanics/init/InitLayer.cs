using UnityEngine;

public class InitLayer : InitGizmos
{
    protected override void Start()
    {
        base.Start();
        cf2D.useTriggers = false;
        cf2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        cf2D.useLayerMask = true;
    }
}