using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private GameObject _playerPrfab;
    [Header("Obstical Variables")]
    

    [Header("Collectable variables")]
    [SerializeField] private GameObject _collectablePrefab;
    [SerializeField] private int _collectablePoolSize;
    private List<GameObject> _collectablePool;
    [SerializeField] private Vector3 _pickupWeight;

    

    private Events _event;

    private bool _gameActive = false;

   

    
    private void Update()
    {
        if (_gameActive)  // if game active start spawning obsticals on a given Delay
        {
            

        }
    }

   
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
      

        
    }

    private void StartGame()  // when level start is called obstacle pool is initialized
    {

        Instantiate(_playerPrfab, GameResources.playerStartPos, _playerPrfab.transform.rotation);
        _gameActive = true;
    }

 /*   private void InitializeObstaclePool() // initialize obstacle pool
    {
        for(int i = 0; i <= _obstaclePoolSize; i++)
        {
            AddOstacleToPool();
        }
    }

    private GameObject AddOstacleToPool() // adds obstacle to pool
    {
        var d = Instantiate(_obstacle, _poolStore, transform.rotation);
        _obstaclePool.Add(d);
        d.GetComponent<Obstacle>().Initialize();
        return d;
    }

    private void InitializeCollectablePool() // initialize obstacle pool
    {
        for (int i = 0; i <= _collectablePoolSize; i++)
        {
            AddCollectableToPool();
        }
    }

    private GameObject AddCollectableToPool() // adds obstacle to pool
    {
        var d = Instantiate(_collectablePrefab, _poolStore, transform.rotation);
        _collectablePool.Add(d);
        d.GetComponent<Collectable>().Initialize();
        return d;
    }*/


    

    private void OnEnable()   // when the spawn manager is enabled it registers to recieve events
    {
        _event = Events.Instance;
        _event.onLevelReadyToStart += StartGame;
       
        
    }

    public void OnGameOver()  // when the game is over destroy all the pool objects
    {
        _gameActive = false;
       
    }

   
    private void LevelChanged(int no)
    {
        Debug.Log("Spawn Manager next level called");
        _gameActive = false;
    }


 

    private void StartNextLevel()
    {
        _gameActive = true;
    }
}
