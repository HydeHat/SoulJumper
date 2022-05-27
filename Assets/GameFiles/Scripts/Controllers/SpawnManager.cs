using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{

    [SerializeField] GameObject[] _entityPrefabs;
    

    private Events _event;

    private bool _gameActive = false;

   

    
    private void Update()
    {

    }

   
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
      

        
    }

    private void StartGame()  // when level start is called obstacle pool is initialized
    {

    }

 
    

    private void OnEnable()   // when the spawn manager is enabled it registers to recieve events
    {
   //     _event = Events.Instance;
     //   _event.onLevelReadyToStart += StartGame;
       
        
    }


    public void SpawnEnemies(SpawnPoint[] spawnPoints)
    {
        foreach(SpawnPoint sp in spawnPoints)
        {
            GameObject character = PickRandomEntity();
            Vector3 pos = sp.gameObject.transform.position;
            pos = new Vector3(pos.x,pos.y + 1.0f, pos.z);


            GameObject entity = Instantiate(character, pos, sp.gameObject.transform.rotation);
            if(sp.GetSpawnType() == GameResources.spawnType.GUARDING)
            {
                entity.GetComponent<EnemyAi>().isGuarding = true;
            }
        }
    }

    private GameObject PickRandomEntity()
    {
        int randomIndex = Random.Range(0, _entityPrefabs.Length - 1);
        return _entityPrefabs[randomIndex];
    }
}
