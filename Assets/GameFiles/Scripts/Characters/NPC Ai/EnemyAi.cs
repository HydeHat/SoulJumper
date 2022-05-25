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
    private bool _isPatrolling;

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

    //if no enemy in range
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
        _isPatrolling = true;
        _gun.SetFiring(false);
    }

    //Walk Random route
    private void SearchWalkPoint()
    {
        //Calculate range
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    //Track enemy
    private void ChasePlayer()
    {
        
        _agent.isStopped = false;
        _agent.SetDestination(GameResources.playerTransform.position);
        _gun.SetFiring(false);
        _isPatrolling = false;

    }

    //Stop and attack the player once in range
    private void AttackPlayer()
    {
        _agent.isStopped = true;
        Vector3 lookDirection = new Vector3(GameResources.playerTransform.position.x, transform.position.y, GameResources.playerTransform.position.z);
        transform.LookAt(lookDirection);
        _isPatrolling = false;
        _gun.SetFiring(true);
    }

    //when killed
    public void OnDeath()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_controlledEntity.isPossesed == 0)
        {
            if (!_agent.isStopped)
            {
                if (_controlledEntity._charAnimControl != null)
                {
                    if (_isPatrolling)
                    {
                        _controlledEntity._charAnimControl.SetBlend(1, 0);
                    }
                    else
                    {
                        _controlledEntity._charAnimControl.SetBlend(1, 1);
                    }
                    Vector3 nextMovePoint = _agent.nextPosition;
                    _controlledEntity._charAnimControl.PlayAnimation(nextMovePoint);
                }
            }
            else
            {
                if (_controlledEntity._charAnimControl != null)
                {
                    _controlledEntity._charAnimControl.SetBlend(1, 1);
                    _controlledEntity._charAnimControl.PlayAnimation(Vector3.zero);
                }
            }
        }
    }

}
