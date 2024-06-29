using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCountSliderUI : ResourceCountUI
{
    [SerializeField] private Slider slider;
    protected override void OnResourceUpdate(float value)
    {
        if (slider != null) slider.value = value;
    }
}
