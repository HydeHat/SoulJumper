using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public float distancetoMove;
    public float speed;
    public bool _isMoving = false;

  
    private float _stopPosY;

    private void Start()
    {
        _stopPosY = transform.position.y - distancetoMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>())
        {
            if(other.GetComponent<Entity>().isPossesed == 1)
            {
                _isMoving = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {

            
            if ( transform.position.y > _stopPosY)
            {
                transform.Translate(Vector3.back * (speed * Time.deltaTime));
            }
            else { _isMoving = false; }
        }
        
    }
}
