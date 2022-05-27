using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameResources.spawnType type;

    public GameResources.spawnType GetSpawnType()
    {
        return type;
    }
}
