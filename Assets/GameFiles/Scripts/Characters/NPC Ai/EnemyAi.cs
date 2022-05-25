using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;



    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private int hitPointsRemaining;


    [SerializeField] private Gun _gun;
    private GameObject _entity;
    private Entity _controlledEntity;

    [Header("Patrol settigs")]
    //Patroling variables
    private Vector3 _walkPoint;
    private bool walkPointSet;
    [SerializeField] private float _walkPointRange;

    //states
    [SerializeField] private float _sightRange, _attackRange;
    [SerializeField] private bool _playerInSightRange, _playerInAttackingRange, _playerVisible;

    private void Awake()
    {

        _agent = GetComponent<NavMeshAgent>();
        _gun = GetComponentInChildren<Gun>();
        _entity = GetComponentInChildren<Entity>().gameObject;
        _controlledEntity = GetComponentInChildren<Entity>();

    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (_controlledEntity.isPossesed == 0)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            _playerVisible = false;
            //check for sight and attack range
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, whatIsPlayer);
            _playerInAttackingRange = Physics.CheckSphere(transform.position, _attackRange, whatIsPlayer);
            if (GameResources.playerTransform != null)
            {
                Vector3 direction = GameResources.playerTransform.position - transform.position;

                if (_playerInSightRange)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, 30f))
                    {
                        if (hit.collider.GetComponent<Entity>())
                        {
                            if (hit.collider.GetComponent<Entity>().isPossesed == 1)
                            {
                                _playerVisible = true;
                            }
                        }
                    }
                }
            }
            if (!_playerInSightRange && !_playerInAttackingRange && !_playerVisible) Patroling();
            if (_playerInSightRange && !_playerInAttackingRange && !_playerVisible) Patroling();
            if (_playerInSightRange && _playerInAttackingRange && !_playerVisible) Patroling();

            if (_playerInSightRange && !_playerInAttackingRange && _playerVisible) ChasePlayer();
            if (_playerInAttackingRange && _playerVisible) AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            _agent.SetDestination(_walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;
        //walkpoint reached
        if(distanceToWalkPoint.magnitude < 1.0f)
        {
            walkPointSet = false;
        }
        _gun.SetFiring(false);
    }

    private void SearchWalkPoint()
    {
        //Calculate range
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        
        _agent.isStopped = false;
        _agent.SetDestination(GameResources.playerTransform.position);
        _gun.SetFiring(false);

    }

    private void AttackPlayer()
    {
        _agent.isStopped = true;
        Vector3 lookDirection = new Vector3(GameResources.playerTransform.position.x, transform.position.y, GameResources.playerTransform.position.z);
        transform.LookAt(lookDirection);

        _gun.SetFiring(true);
    }


    public void OnDeath()
    {
        Destroy(gameObject);
    }

 
}
