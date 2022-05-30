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
    private bool _isAlien = false;

    public bool IsAlien()
    {
        return _isAlien;
    }
    private void Start()
    {
        _event = Events.Instance;
        GameResources._player = this;
        LevelStartSetup();
    }

    public void LevelStartSetup()
    {
        Debug.Log("Player start level called");
        _firstEntity = GameResources.currentLevelStartSettings.firstEntity;
        _isAlien = GameResources.currentLevelStartSettings.isAlien;
        _firstEntity.SetPossesed(1, _isAlien);
        GameResources.playerTransform = _firstEntity.transform;
        _currentEntityTransform = _firstEntity.transform;
        _currentPossesedEntity = _firstEntity;
        gun = _currentPossesedEntity.ReturnGun();
    }



    private void Update()
    {
        if (GameResources.gameIsRunning)
        {
            if (!_isAlien)
            {
                if (Input.GetMouseButton(0))
                {
                    if (gun != null)
                        gun.SetFiring(true);
                }
                else
                {
                    if (gun != null)
                        gun.SetFiring(false);
                }

                if (Input.GetMouseButton(0))
                {
                    if (_soulJumpCharge >= 100)
                    {
                        _currentPossesedEntity.NowDead();
                        JumpToNewBody();

                    }
                }
                if (_soulJumpCharge < 100f)
                {
                    _soulJumpCharge += _soulJumpChargeRate * Time.deltaTime;
                }
                if (_soulJumpCharge > 100f)
                {
                    _soulJumpCharge = 100f;
                }

                _event.PlayerJumpChanged(Mathf.RoundToInt(_soulJumpCharge));
            }
        }
    }



    private void JumpToNewBody()
    {
        if (_isAlien) _isAlien = false; 
        float closestDistance = 10000000000000000f;
        foreach (GameObject obj in GameResources.entities)
        {
            if (obj)
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
        _currentPossesedEntity.SetPossesed(1, _isAlien);
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
        GameManager.Instance.LoadLevel("GameOver");
    
    }

    private void OnEnable() // trigger update of gold and lives UI
    {
        if(_event == null)
        {
            _event = Events.Instance;
        }
       
    }



    private void OnTriggerEnter(Collider other) 
    {
        
    }



    

    private void GameOver()
    {
        GameManager.Instance.LoadLevel("GameOver");
        gameObject.SetActive(false);
    }

    

    private void StartNextLevel()
    {
            
    }

}
