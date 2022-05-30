using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Character stats")]
    private int _totalHitpoints;
    [SerializeField] private int _hitPointsRemaing;
    [SerializeField] public int _speed;
    
    //Private Variables/ conrollers

    private Gun _gun;
    private EnemyAi _enemyAi;
    private Rigidbody _rb;
    private int _entityIndex;
    public CharAnimControl _charAnimControl;
    public bool isAlive = true;
    public bool isAlien = false;
    //Pulic Variables
    public int isPossesed = 0 ;

    
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _gun = GetComponentInChildren<Gun>();
        _enemyAi = GetComponent<EnemyAi>();
         GameResources.entities.Add(gameObject);
        _entityIndex = GameResources.nunberOfEntities;
        GameResources.nunberOfEntities++;
        if (GetComponentInChildren<CharAnimControl>())
        {
            _charAnimControl = GetComponentInChildren<CharAnimControl>();
        }
        else
        {
            Debug.Log("Character does not have a _charAnimContoller.");
        }
        _totalHitpoints = _hitPointsRemaing;
    }

    public int GetHitPointsRemaing()
    {
        return _hitPointsRemaing;
    }
    public int GetHitPointsTotal()
    {
        return _totalHitpoints;
    }
    public void OnHit(int damage)
    {
        if (_hitPointsRemaing > 0)
        {
            _hitPointsRemaing -= damage;
            if (_hitPointsRemaing <= 0)
            {
                if (isPossesed == 0)
                {
                    _enemyAi.OnDeath();
                }
                else
                {
                    GameResources._player.OnDeath();
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public Gun ReturnGun()
    {
        return _gun;
    }

    public void SetPossesed(int isPos, bool alien)
    {
        isPossesed = isPos;
        isAlien = alien;
        SetGameobjectLayer();
        if (GameResources.nunberOfEntities > 0)
        {
            GameResources.entities.Remove(gameObject);
            GameResources.nunberOfEntities--;
        }
    }

    private void SetGameobjectLayer()
    {
        LayerMask mask = LayerMask.NameToLayer("whatIsPlayer");
        gameObject.layer = mask;
    }

    public void NowDead()
    {
        isAlive = false;
        gameObject.GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
        isPossesed = 0;
    }
}
