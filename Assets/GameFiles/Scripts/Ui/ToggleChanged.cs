using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChanged : MonoBehaviour
{
    public Toggle toggle;
    public GameResources.volumeType type;

    public void OnToggled()
    {
        UIManager.Instance.AudioToggled(type, toggle.isOn);
    }
}
