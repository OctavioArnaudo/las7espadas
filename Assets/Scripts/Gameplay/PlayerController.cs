public class PlayerController : MonoController
{
    private Move2dV12 _move2d;
    private SpriteFlipV11 _spriteFlip;

    protected override void Awake()
    {
        base.Awake();
        _move2d = new Move2dV12(horizontalInput, moveSpeed, rb, displacementInput, isMoving: false);
        _spriteFlip = new SpriteFlipV11(displacementInput, horizontalInput, displacementOrientation, sr, isFlippedX: false);
    }

    protected override void Update()
    {
        base.Update();

        _move2d.OnUpdate();
        _move2d.isMoving = isMoving;
        isMoving = _move2d.isMoving;
        _move2d.horizontalInput = horizontalInput;
        horizontalInput = _move2d.horizontalInput;
        _move2d.moveSpeed = moveSpeed;
        moveSpeed = _move2d.moveSpeed;
        _move2d.rb = rb;
        rb = _move2d.rb;

        _spriteFlip.OnUpdate();
        _spriteFlip.isFlippedX = isFlippedX;
        isFlippedX = _spriteFlip.isFlippedX;
        _spriteFlip.displacementInput = displacementInput;
        displacementInput = _spriteFlip.displacementInput;
        _spriteFlip.horizontalInput = horizontalInput;
        horizontalInput = _spriteFlip.horizontalInput;
        _spriteFlip.sr = sr;
        sr = _spriteFlip.sr;

    }

}