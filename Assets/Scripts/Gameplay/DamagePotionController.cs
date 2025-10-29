public class DamagePotionController : MonoController
{
    private SpriteFlipV11 _spriteFlip;

    protected override void Awake()
    {
        base.Awake();
        _spriteFlip = new SpriteFlipV11(displacementInput, horizontalInput, displacementOrientation, sr, false);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _spriteFlip.OnUpdate();
    }

}