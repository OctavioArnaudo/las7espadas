using UnityEngine;

public class SpriteFlipV2 : MonoModular
{
    private Transform _transform;
    public Transform transform
    {
        get => _transform;
        set => _transform = value;
    }

    private bool _isFlippedX;
    public bool isFlippedX
    {
        get => _isFlippedX;
        set => _isFlippedX = value;
    }

    public SpriteFlipV2(
        Transform transform,
        bool isFlippedX
        )
    {
        this.transform = transform;
        this.isFlippedX = isFlippedX;
    }

    public virtual void OnUpdate()
    {
        Vector3 scaler = transform.localScale;
        scaler.x = isFlippedX ? -Mathf.Abs(scaler.x) : Mathf.Abs(scaler.x);
        transform.localScale = scaler;
    }
}