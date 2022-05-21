using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Material _outLineMaterial;
    [SerializeField] Material _basicMaterial;

    public void SetOutline(bool active)
    {
        
        if (active)
        {
            _renderer.material = _outLineMaterial;


        }
        else
        {
            _renderer.material = _basicMaterial;
            active = false;
        }
    }

}
