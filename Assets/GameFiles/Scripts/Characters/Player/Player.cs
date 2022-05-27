using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> 
{
    [SerializeField] private Events _event;
    //public Events.EventLevelChanged levelChanged;

    [SerializeField] private GameObject entity;
    [SerializeField] private GameObject _soulPrefab;
    [SerializeField] private bool _targetReached;
    //[SerializeField] private PlayerWeapons _playWep;
    //playerStats
    [Header("Player stats")]
    [SerializeField] private float _soulJumpCharge = 100;
    [SerializeField] private float _soulJumpChargeRate = 5.0f;
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
        gun = _currentPossesedEntity.ReturnGun();
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

        if(_soulJumpCharge < 100f)
        {
            _soulJumpCharge += _soulJumpChargeRate * Time.deltaTime;
        }
    }



    private void JumpToNewBody()
    {
        float closestDistance = 10000000000000000f;
        foreach (GameObject obj in GameResources.entities)
        {
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

        StartCoroutine( PossessEntity());

    }

    IEnumerator  PossessEntity()
    {
        _targetReached = false;
        Entity oldEntity = _currentPossesedEntity;
        Transform oldETans = _currentEntityTransform;
        _currentPossesedEntity = _closestEntity.GetComponent<Entity>();
        _currentEntityTransform = _closestEntity.transform;
        oldEntity.NowDead();
        GameObject soul = Instantiate(_soulPrefab, oldETans.position , oldETans.rotation);
        soul.GetComponent<SoulMovement>().SetToPossesedTransform(_currentEntityTransform);
        yield return new WaitUntil(() => _targetReached == true);
        _currentPossesedEntity.SetPossesed(1);
        GameResources.playerTransform = _currentEntityTransform;
        oldEntity.NowDead();
        gun = _currentPossesedEntity.ReturnGun();
        _soulJumpCharge = 0f;
    }

    public void SetTargetReached(bool targ)
    {
        _targetReached = targ;
    }

    public void OnDeath()
    {
        if(_soulJumpCharge >= 100)
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
