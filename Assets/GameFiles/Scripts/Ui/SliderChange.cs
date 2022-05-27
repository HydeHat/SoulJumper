using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    [SerializeField] private GameResources.volumeType _volumeType;
    [SerializeField] private UIManager manager;
    [SerializeField] private Slider slider;

    [SerializeField] private Toggle toggle;

    public void SliderChanged()
    {
        manager.VolumeChanger(_volumeType, slider.value);
    }

    public void ToggleChange()
    {
        manager.AudioToggled(_volumeType, toggle.isOn);
    }

}
