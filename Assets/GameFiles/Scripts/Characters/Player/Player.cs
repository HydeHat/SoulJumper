using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> 
{
    [SerializeField] private Events _event;
    //public Events.EventLevelChanged levelChanged;

    [SerializeField] private GameObject entity;
    //[SerializeField] private PlayerWeapons _playWep;
    //playerStats
    [Header("Player stats")]
    [SerializeField] private int _soulCharge;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private int hitPointsLeft;

    [Space(3)]

    [SerializeField] Gun gun;

    //private int _lastLevelCheckpoint = 0;
    private GameObject _closestEntity;
    private Transform _currentEntityTransform;
    private Entity _currentPossesedEntity;

    [SerializeField] private Entity _firstEntity;


    private void Start()
    {
        _event = Events.Instance;
        _firstEntity.SetPossesed(1);
        GameResources.playerTransform = _firstEntity.transform;
        _currentEntityTransform = _firstEntity.transform;
        _currentPossesedEntity = _firstEntity;
        GameResources._player = this;

    }

    private void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            gun.SetFiring(true);
        }
        else
        {
            gun.SetFiring(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            JumpToNewBody();
        }
    }

    private void JumpToNewBody()
    {
        float closestDistance = 10000000000000000f;
        foreach (GameObject obj in GameResources.entities)
        {
            Debug.Log("in loop");
            if (GameResources.entities.Count > 1)
            {

                float distance = Vector3.Distance(_currentEntityTransform.position, obj.transform.position);
                if (distance <= closestDistance)
                {
                    closestDistance = distance;
                    _closestEntity = obj;

                }

            }
            else
            {
                _closestEntity = obj;
            }
        }

        PossessEntity();

    }

    private void PossessEntity()
    {
        Entity oldEntity = _currentPossesedEntity;
        _currentPossesedEntity = _closestEntity.GetComponent<Entity>();

        _currentPossesedEntity.SetPossesed(1);
        _currentPossesedEntity.isPossesed = 1;
        _currentEntityTransform = _closestEntity.transform;
        GameResources.playerTransform = _currentEntityTransform;
        oldEntity.DestroyMe();
        gun = _currentPossesedEntity.ReturnGun();
    }

    public void OnDeath()
    {
        if(_soulCharge >= 100)
        {
            JumpToNewBody();
        }
        else
        {
            OnFullDeath();
        }
    }

    private void OnFullDeath()
    {
        // add stuff
    
    }

    private void OnEnable() // trigger update of gold and lives UI
    {
       /* if(_event == null)
        {
            _event = Events.Instance;
        }
        */
    }



    private void OnTriggerEnter(Collider other) 
    {
        
    }



    

  /*  private void GameOver()
    {
        GameManager.Instance.LoadLevel("GameOver");
        SpawnManager.Instance.OnGameOver();
        gameObject.SetActive(false);
    }

    

    private void StartNextLevel()
    {
            
    }
    */
}
