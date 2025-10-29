using UnityEngine;

public class SpriteFlipV11 : SpriteFlipV1
{
    private Vector2 _displacementInput;
    public Vector2 displacementInput
    {
        get => _displacementInput;
        set => _displacementInput = value;
    }

    private float _horizontalInput;
    public float horizontalInput
    {
        get => _horizontalInput;
        set => _horizontalInput = value;
    }

    private SpriteModel _displacementOrientation;
    public SpriteModel displacementOrientation
    {
        get => _displacementOrientation;
        set => _displacementOrientation = value;
    }

    private bool _isFlippedX;
    public bool isFlippedX
    {
        get => _isFlippedX;
        set => _isFlippedX = value;
    }

    private SpriteRenderer _sr;
    public SpriteRenderer sr
    {
        get => _sr;
        set => _sr = value;
    }

    public SpriteFlipV11(
        Vector2 displacementInput,
        float horizontalInput,
        SpriteModel displacementOrientation,
        SpriteRenderer sr,
        bool isFlippedX
        ) : base (isFlippedX, sr)
    {
        this.displacementInput = displacementInput;
        this.horizontalInput = horizontalInput;
        this.displacementOrientation = displacementOrientation;
        this.isFlippedX = isFlippedX;
        this.sr = sr;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // If the horizontal input is greater than 0.01, set the orientation to Right.
        if (displacementInput.x > 0.01f || horizontalInput > 0f)
        {
            isFlippedX = (displacementOrientation == SpriteModel.Left);
            SpriteFlipV1 instance = new SpriteFlipV1(isFlippedX, sr);
            instance.OnUpdate();
        }
        // Check if the horizontal input is within a small range around zero to determine if the orientation should be Centered.
        else if ((displacementInput.x < 0.01f && displacementInput.x > -0.01f) || horizontalInput == 0f)
        {
            isFlippedX = (displacementOrientation == SpriteModel.Centered);
            SpriteFlipV1 instance = new SpriteFlipV1(isFlippedX, sr);
            instance.OnUpdate();
        }
        // If the horizontal input is less than -0.01, set the orientation to Left.
        else if (displacementInput.x < -0.01f || horizontalInput < 0f)
        {
            isFlippedX = (displacementOrientation == SpriteModel.Right);
            SpriteFlipV1 instance = new SpriteFlipV1(isFlippedX, sr);
            instance.OnUpdate();
        }
    }

}