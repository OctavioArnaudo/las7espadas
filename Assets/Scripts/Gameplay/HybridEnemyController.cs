public class HybridEnemyController : MonoController
{
    private Move2dV11 _move2dIn4d;
    private SpriteFlipV11 _spriteFlip;

    protected override void Awake()
    {
        base.Awake();
        _spriteFlip = new SpriteFlipV11(displacementInput, horizontalInput, displacementOrientation, sr, false);
        _move2dIn4d = new Move2dV11(rb, moveSpeed, displacementInput, isMoving);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _move2dIn4d.OnUpdate();
        _spriteFlip.OnUpdate();
    }

}