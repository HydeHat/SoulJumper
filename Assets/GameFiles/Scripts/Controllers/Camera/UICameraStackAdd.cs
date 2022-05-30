using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class UICameraStackAdd : MonoBehaviour
{
    Camera main;
    public Camera cameraThis;
    
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        var cameraData = main.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(cameraThis);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
