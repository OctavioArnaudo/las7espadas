using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderBar : MonoBehaviour
{
    protected Slider slider;
    protected float health;

    protected void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 3;
        SetSliderMaxValue(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Health Depleted");
            Destroy(gameObject);
        }
    }

    public void SetSliderMaxValue(float value)
    {
        slider.maxValue = value;
        SetSliderValue(value);
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }

    protected void OnMouseDown()
    {
        Debug.Log("Mouse Down on SliderBar");
        health -= 1;
        SetSliderValue(health);
    }

}
