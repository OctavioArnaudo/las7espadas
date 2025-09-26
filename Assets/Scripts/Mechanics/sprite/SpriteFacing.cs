public class SpriteFacing : SpriteScaling
{
    protected override void Awake()
    {
        base.Awake();
        flipX = (bool xFlipValue) => {
            sr.flipX = xFlipValue;
        };
    }
}