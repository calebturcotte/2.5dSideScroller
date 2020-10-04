using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;


    public void SetMaxHealth(int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;

        fill.color = gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
