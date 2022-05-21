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

    [Space(3)]

    [SerializeField] Gun gun;

    private int _lastLevelCheckpoint = 0;
    private Transform playerTrans;
    private GameObject closestEntity;



    private void Start()
    {
        playerTrans = transform;
        _event = Events.Instance;
        playerTrans.position = entity.transform.position + _playerOffset;
        entity.transform.SetParent(this.transform);
        

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
            
            float closestDistance = 10000000000000000f;
            GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");
            foreach(GameObject obj in entities)
            {
                if (obj != entity)
                {
                    float distance = Vector3.Distance(playerTrans.position, obj.transform.position);
                    if(distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEntity = obj;
                    }
                }
            }

            entity.transform.SetParent(null);
            playerTrans.position = closestEntity.transform.position + _playerOffset;
            entity = closestEntity;
            entity.transform.SetParent(playerTrans);
        }
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

   /* private void LifeLost() // functions when life los
    {
        
        // needs function for game over when all the lives are lost
    }*/

    

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
