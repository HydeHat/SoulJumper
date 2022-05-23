using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Character stats")]
    [SerializeField] private int _hitPointsRemaing;
    [SerializeField] public int _speed;
    [SerializeField] private LayerMask _playerLayermask;

    private Gun _gun;
    private EnemyAi _enemyAi;
    private Rigidbody _rb;
    private Player _player;
    public bool isPossesed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _gun = GetComponentInChildren<Gun>();
        _enemyAi = GetComponent<EnemyAi>();
        _player = GameObject.FindObjectOfType<Player>();
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
                _player.OnDeath();
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
        gameObject.layer = 7;

    }
}
