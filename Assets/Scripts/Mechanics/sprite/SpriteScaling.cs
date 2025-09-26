using System;
using UnityEngine;

public class SpriteScaling : ActionReleased
{
    protected Action<bool> flipX;
    protected override void Awake()
    {
        base.Awake();
        flipX = (bool xFlipValue) => {
            Vector3 scaler = transform.localScale;
            scaler.x = xFlipValue ? -Mathf.Abs(scaler.x) : Mathf.Abs(scaler.x);
            transform.localScale = scaler;
        };
    }
}