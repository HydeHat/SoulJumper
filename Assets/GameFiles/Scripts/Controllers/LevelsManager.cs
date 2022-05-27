using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : Singleton<LevelsManager>
{


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {



    }
}
