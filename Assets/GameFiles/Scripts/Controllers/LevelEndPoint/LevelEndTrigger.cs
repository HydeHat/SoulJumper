using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{


    private Events _events;

    private void Awake()
    {
        _events = Events.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>())
        {
            GameResources.currentLevel++;
            _events.SetChangeLevel(GameResources.currentLevel);
        }
    }
}
