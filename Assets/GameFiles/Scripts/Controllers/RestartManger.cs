using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class RestartManger : MonoBehaviour
{
    public void Restart(GameObject[] managers)
    {

        foreach(GameObject o in managers)
        {
            Destroy(o);
        }
        Destroy(GameManager.Instance.gameObject);
        Destroy(Camera.main.gameObject);
        SceneManager.LoadSceneAsync("Title");


    }

}
