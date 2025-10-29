using UnityEngine;

public class InitCamera : InitButtons
{
    protected Vector3 screenPosition;

    protected override void Start()
    {
        base.Start();
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //fov = vcam.Lens.FieldOfView;
    }
}