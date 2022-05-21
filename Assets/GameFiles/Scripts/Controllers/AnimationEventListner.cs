using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListner : MonoBehaviour
{
    UIManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = UIManager.Instance;
    }

    public void FadeOut(GameResources.uiGroupName name)
    {

        _manager.FadeOutComplete(name);
    }
        public void FadeIn(GameResources.uiGroupName name)
    {
        _manager.FadeInComplete(name);
    }

}
