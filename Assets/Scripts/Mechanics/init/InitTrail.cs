public class InitTrail : InitTile
{
    protected override void OnEnable()
    {
        base.OnEnable();
        trail.Clear();
        trail.enabled = true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        trail.enabled = false;
    }

}