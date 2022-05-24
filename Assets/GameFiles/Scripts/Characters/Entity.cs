using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Character stats")]
    [SerializeField] private int _hitPointsRemaing;
    [SerializeField] public int _speed;

    private Gun _gun;
    private EnemyAi _enemyAi;
    private Rigidbody _rb;
    private int _entityIndex;
    
    public bool isPossesed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _gun = GetComponentInChildren<Gun>();
        _enemyAi = GetComponent<EnemyAi>();
         GameResources.entities.Add(gameObject);
        Debug.Log("Added to entities");
        _entityIndex = GameResources.nunberOfEntities;
        GameResources.nunberOfEntities++;
    }


    public void OnHit(int damage)
    {
        _hitPointsRemaing -= damage;
        if (_hitPointsRemaing <= 0)
        {
            if (!isPossesed)
            {
                _enemyAi.OnDeath();
            }
            else
            {
                GameResources._player.OnDeath();
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

    public void SetPossesed(bool isPos)
    {
        isPossesed = isPos;
        SetGameobjectLayer();
        Debug.Log(GameResources.entities.Count);
        GameResources.entities.RemoveAt(_entityIndex);

        Debug.Log(GameResources.entities.Count);
        GameResources.nunberOfEntities--;

    }

    private void SetGameobjectLayer()
    {
        LayerMask mask = LayerMask.NameToLayer("whatIsPlayer");
        gameObject.layer = mask;
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
