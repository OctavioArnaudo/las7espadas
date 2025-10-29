using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BaseSlider : BaseRigidbody2D
{
    [SerializeField] protected Slider slider;

    protected override void Awake()
    {
        base.Awake();
        slider = GetComponent<Slider>();
    }

    public IEnumerator SetSliderMaxValue(float value)
    {
        slider.maxValue = value;
        SetSliderValue(value);
        yield return null;
    }

    public IEnumerator SetSliderValue(float value)
    {
        slider.value = value;
        yield return null;
    }
}
