using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderView : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void setSliderValue(float value)
    {
        slider.value = value;
    }

}
