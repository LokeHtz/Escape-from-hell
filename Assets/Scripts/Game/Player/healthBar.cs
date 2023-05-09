using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
