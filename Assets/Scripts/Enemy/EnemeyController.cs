using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
//using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class EnemeyController : MonoBehaviour
{
    enum EnemyState
    {
        Patrol = 0,
        Investigate = 1,
        Stunned = 2
    }
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _threshold = 0.5f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _stunnedTime = 3f;
    [SerializeField] private PatrolRoute _patrolRoute;                                           //serialize our monobehvior we made, will only take PatrolRoute
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private EnemyState _state = EnemyState.Patrol;

    public UnityEvent<Transform> onPlayerFound;
    public UnityEvent onInvestigate;
    public UnityEvent onReturnToPatrol;
    public UnityEvent onStunned;
    
    private bool _moving = false;
    private Transform _currentPoint;
    private int _routeIndex = 0;
    private bool _forwardAlongPath = true;
    private Vector3 _investigationPoint;
    private float _waitTimer = 0f;
    private bool _playerFound = false;
    private float _stunnedTimer = 0f;

    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }
    
    void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        
        if (_fov.visibleObjects.Count > 0)
        {
            Debug.Log(_fov.visibleObjects[0]);
            PlayerFound(_fov.visibleObjects[0].position);  
        }
        if (_state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
        else if(_state == EnemyState.Investigate)
        {
            UpdateInvestigate();
        }
        else if (_state == EnemyState.Stunned)
        {
            _stunnedTimer += Time.deltaTime;
            if (_stunnedTimer >= _stunnedTime)
            {
                ReturnToPatrol();
                _animator.SetBool("Stunned", false);
            }
        }
       
    }

    public void SetStunned()
    {
        _animator.SetBool("Stunned", true);
        _stunnedTimer = 0f;
        _state = EnemyState.Stunned;
        _agent.SetDestination(transform.position);                                                     //stop him from moving     
        onStunned.Invoke();
    }

    public void InvestigatePoint(Vector3 investigatePoint)                                             //runs once sometimes, if it's audio trigger just go and investigate it
    {
        //Debug.Log("Investigating Point Trigger");
        SetInvestigationPoint(investigatePoint);

        onInvestigate.Invoke();
    }

    private void SetInvestigationPoint(Vector3 investigatePoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigatePoint;
        _agent.SetDestination(_investigationPoint);
    }

    private void PlayerFound(Vector3 investigatePoint)
    {
        if (_playerFound) return;
        
        SetInvestigationPoint(investigatePoint);
        
        Debug.Log("The enemy creature's head is: "+ _fov.creature.head);
        onPlayerFound.Invoke(_fov.creature.head);  

        _playerFound = true;
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
        
        onReturnToPatrol.Invoke();
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
