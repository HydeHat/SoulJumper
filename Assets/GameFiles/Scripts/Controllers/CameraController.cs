using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        if (GameResources.playerTransform != null)
        {
            transform.position = GameResources.playerTransform.position + cameraOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameResources.playerTransform != null)
        {
            transform.position = GameResources.playerTransform.position + cameraOffset;
        }
    }
}
