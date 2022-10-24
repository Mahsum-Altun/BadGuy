using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthNPC : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fiil;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fiil.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fiil.color = gradient.Evaluate(slider.normalizedValue);
    }
}
