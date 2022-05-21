using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : Singleton<LevelsManager>
{
    public Level[] levels;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {


        if(levels.Length == 0)
        {
            levels = gameObject.GetComponents<Level>();
        }
    }
}
