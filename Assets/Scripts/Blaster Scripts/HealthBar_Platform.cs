using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Platform : MonoBehaviour
{

    public Slider sliderPlatform;

    public void SetMaxHealth(int health)
    {
        sliderPlatform.maxValue = health;
        sliderPlatform.value = health;
    }

    public void SetHealth(int health)
    {
        sliderPlatform.value = health;
    }
}
