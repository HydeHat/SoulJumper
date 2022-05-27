using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    public float speed;

    private Transform _toPosse;
    private Transform _soalTran;

    private void Start()
    {
        _soalTran = gameObject.transform;
    }

    private void Update()
    {
        if (_toPosse != null)
        {
            
            Vector3 direction =  _toPosse.position - _soalTran.position;
            if (direction.magnitude >= .1)
            {
                _soalTran.position = Vector3.MoveTowards( _soalTran.position,_toPosse.position,  speed * Time.deltaTime);
            }
            else
            {
                ReachedDestination();
            }

        }
    }

    public void SetToPossesedTransform(Transform tr)
    {
        _toPosse = tr;
    }

    private void ReachedDestination()
    {
        GameResources._player.SetTargetReached(true);
        Destroy(gameObject);
    }
}
