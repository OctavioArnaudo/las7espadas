using UnityEngine;

public class SpriteFlipV1 : MonoModular
{
    private SpriteRenderer _sr;
    public SpriteRenderer sr
    {
        get => _sr;
        set => _sr = value;
    }

    private bool _isFlippedX;
    public bool isFlippedX
    {
        get => _isFlippedX;
        set => _isFlippedX = value;
    }

    public SpriteFlipV1(
        bool isFlippedX,
        SpriteRenderer sr
        )
    {
        this.sr = sr;
        this.isFlippedX = isFlippedX;
    }
    
    public virtual void OnUpdate()
    {
        sr.flipX = isFlippedX;
    }
}