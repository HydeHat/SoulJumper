using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] SpawnPoint[] _spawnPoints;
    
    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponentInParent<Entity>())
        {
            if (other.gameObject.GetComponentInParent<Entity>().isPossesed == 1)
            {

                if (!_hasTriggered)
                {
                    if (_spawnPoints.Length > 0)
                    {
                        SpawnManager.Instance.SpawnEnemies(_spawnPoints);
                    }
                    _hasTriggered = true;
                }
            }
        }
    }
}
