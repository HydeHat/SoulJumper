using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class DoorControll : MonoBehaviour
{
    public int soundToPlay;
    private Transform door;
    [SerializeField] private bool _open;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>())
        {
            _open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Entity>())
        {
            _open = false;
        }
    }

    private void Update()
    {
        _anim.SetBool("Open", _open);
    }
}
