using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemeyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float threshold = 0.5f;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;

    private bool moving = false;
    private Transform currentPoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.remainingDistance);
        
        if (!moving)
        {
            if (currentPoint == point1)
            {
                currentPoint = point2;
            }
            else if (currentPoint == point2)
            {
                currentPoint = point3;
            }
            else
            {
                currentPoint = point1;
            }
            agent.SetDestination(currentPoint.position);                                               // this moves the robot agent
            moving = true;
        }

        //Debug.Log("Remaining Distance: " + agent.remainingDistance);
        if (moving && Vector3.Distance(transform.position, currentPoint.position) < threshold)     //agent.remainingDistance is problematic 
        {
            moving = false;
        }
        
    }
}
