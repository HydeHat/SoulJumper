using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Player.transform.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + cameraOffset;
    }
}
