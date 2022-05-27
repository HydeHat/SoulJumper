using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class CharAnimControl : MonoBehaviour
{
    [SerializeField]private Animator _anim;
    private float _turnAmount;
    private float _forwardAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();

    }

    public void SetFiring(bool shoot)
    {
         _anim.SetBool("Firing", shoot);
    }

    public void SetBlend(int blendLayer, float blendAmount)
    {
        _anim.SetLayerWeight(blendLayer, blendAmount);
    }

    public void PlayAnimation(Vector3 move)
    {
        move = move.normalized;
        Vector3 localMove = transform.InverseTransformDirection(move);
        if(Utills.RemoveSign( localMove.x) > Utills.RemoveSign(localMove.z))
        {
            localMove.z = 0;
            if (localMove.x < 0)
            {
                localMove.x = -1f;
            }
            else localMove.x = 1f;
        }
        else if(Utills.RemoveSign(localMove.x) < Utills.RemoveSign(localMove.z))
        {
            localMove.x = 0;
            if(localMove.z < 0)
            {
                localMove.z = -1f;
            }
            else localMove.z = 1f;
        }

        
        _turnAmount = localMove.x;
        _forwardAmount = localMove.z;
        _anim.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        _anim.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
    }

    

}
