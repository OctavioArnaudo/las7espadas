using UnityEngine;
using System;

[Serializable]
public class InitSlider : InitPrefab
{
    protected float health = 3;

    protected override void Start()
    {
        base.Start();
        SetSliderMaxValue(health);
    }

    protected override void Update()
    {
        base.Update();
        if (health <= 0)
        {
            Debug.Log("Health Depleted");
            Destroy(gameObject);
        }
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Mouse Down on SliderBar");
        health -= 1;
        SetSliderValue(health);
    }

}
