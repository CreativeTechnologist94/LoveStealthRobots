using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    enum EnemyState
    {
        Patrol = 0,
        Investigate = 1,
        FindAssistance = 2,
        Idle = 3
    }
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _threshold = 0.5f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private PatrolRoute _patrolRoute;                                           //serialize our monobehvior we made, will only take PatrolRoute
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private EnemyState _state = EnemyState.Patrol;

    private bool _moving = false;
    private Transform _currentPoint;
    private int _routeIndex = 0;
    private bool _forwardAlongPath = true;
    private Vector3 _investigationPoint;
    private float _waitTimer = 0f;
    private GameObject _closestEnemy; 
    private EnemyController closestEnemyController;

    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }
    
    void Update()
    {
        if (_fov.visibleObjects.Count > 0)
        {
            InvestigatePoint(_fov.visibleObjects[0].position);
        }

        if (_state == EnemyState.Idle)
        {
            Debug.Log(transform.gameObject.name+ " is idle");
        }
        else if (_state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
        else if(_state == EnemyState.Investigate)
        {
            UpdateInvestigate();
        }
        else if (_state == EnemyState.FindAssistance)
        {
            Debug.Log("Find Assistance state activated");
            UpdateFindAssistance();
        }
       
    }

    private void UpdateFindAssistance()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 15f);

        if (hitColliders != null && hitColliders.Length > 0)
        {
            Debug.Log("Found collider in sphere");
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.TryGetComponent(out Creature targetCreature) && hitColliders[i].transform!=this.transform)
                {
                    Debug.Log("It's a creature and not self");
                    
                    if (targetCreature.team == Creature.Team.Enemy) 
                    {
                        _closestEnemy = hitColliders[i].gameObject;
                        Debug.Log("Found closest enemy - " + _closestEnemy.name);
                        _agent.SetDestination(_closestEnemy.transform.position);
                        if (Vector3.Distance(transform.position,_closestEnemy.transform.position) < _threshold)
                        {
                            _state = EnemyState.Investigate;
                            _agent.SetDestination(_investigationPoint);
                            
                            closestEnemyController = _closestEnemy.GetComponent<EnemyController>();
                            closestEnemyController._state = EnemyState.Investigate;
                            var _closestEnemyagent = _closestEnemy.GetComponent<NavMeshAgent>();
                            _closestEnemyagent.SetDestination(_investigationPoint);
                        }
                        
                        break;
                    }
                }
            }
        }
    }

    public void InvestigatePoint(Vector3 investigatePoint)                                             //runs once sometimes, if it's audio trigger just go and investigate it
    {
        _investigationPoint = investigatePoint; 
        
        float randValue = Random.value;
        Debug.Log("Random value = "+ randValue);
        if (randValue < .5f)
        {
            _state = EnemyState.Investigate;
            _agent.SetDestination(_investigationPoint);
        }
        else
        {
            _state = EnemyState.FindAssistance;
        }
    }

    private void UpdateInvestigate()                                                                    //if hes chasing us, runs all the time
    {
        Debug.Log("Investigating");
        if (Vector3.Distance(transform.position, _investigationPoint) < _threshold)
        {
            _waitTimer += Time.deltaTime;                                                               //time between each frame
            if (_waitTimer > _waitTime)
            {
                ReturnToPatrol();
            }
        }
    }

    private void ReturnToPatrol()
    {
        Debug.Log("Returning to Patrol");
        _state = EnemyState.Patrol;
        _waitTimer = 0;
        _moving = false;
    }
    
    private void UpdatePatrol()
    {
        if (!_moving)
        {
            NextPatrolPoint();

            _agent.SetDestination(_currentPoint.position); // this moves the robot agent
            _moving = true;
        }

        if (_moving && Vector3.Distance(transform.position, _currentPoint.position) < _threshold) //agent.remainingDistance is problematic 
        {
            _moving = false;
        }
    }

    private void NextPatrolPoint()
    {
        if (_forwardAlongPath)
        {
            _routeIndex++;
        }
        else
        {
            _routeIndex--;
        }
        
        if (_routeIndex == _patrolRoute.route.Count)
        {
            if (_patrolRoute.patrolType == PatrolRoute.PatrolType.Loop)
            {
                _routeIndex = 0;
            }
            else
            {
                _forwardAlongPath = false;
                _routeIndex-=2;
            }
        }

        if (_routeIndex == 0)
        {
            _forwardAlongPath = true;
        }
            
        _currentPoint = _patrolRoute.route[_routeIndex];

    }
}