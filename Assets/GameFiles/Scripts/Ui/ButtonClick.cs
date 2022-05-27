using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{


    public void PlayUISound(int sound)
    {
        Events.Instance.PlayUISoundClip(sound);
    }
}
