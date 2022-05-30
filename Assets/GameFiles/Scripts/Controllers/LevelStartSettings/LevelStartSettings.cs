using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartSettings : MonoBehaviour
{

    public Entity firstEntity;
    public bool isAlien;

    private void Awake()
    {
        GameResources.currentLevelStartSettings = this;
    }

}
