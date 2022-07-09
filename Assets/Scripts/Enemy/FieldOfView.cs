using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

public class FieldOfView : MonoBehaviour
{
    public List<Transform> visibleObjects;
    public Creature creature;     
    
    [SerializeField] private Color _gizmoColor = Color.red;
    [SerializeField] private float _viewRadius = 6f;
    [SerializeField] private float _viewAngle = 30f; //exposing creature to get head height
    [SerializeField] private LayerMask _blockingLayers;                           // exposing layer mask to set blocking layers

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        visibleObjects.Clear();
        
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius);          //Collider[] Returns an array with all colliders touching or inside the sphere
        foreach (Collider target in targetInViewRadius)                                                  //define target of class Collider
        {
            if (!target.TryGetComponent(out Creature targetCreature)) continue;                          // define a targetCreature of class Creature //continue exits the loop //return exits the function

            if (creature.team == targetCreature.team) continue;
            
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;     //normalized direction vector FROM target position TO enemy position
            
            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle)
            {
                Vector3 headPos = creature.head.position;
                Vector3 targetHeadPos = targetCreature.head.position;

                Vector3 dirToTargetHead = (targetHeadPos - headPos).normalized;
                float disToTargetHead = Vector3.Distance(headPos, targetHeadPos);

                if (Physics.Raycast(headPos, directionToTarget, disToTargetHead, _blockingLayers))  //if anything blocks the raycast
                {
                    continue;
                }
                
                Debug.DrawLine(headPos, targetHeadPos, Color.green);

                visibleObjects.Add(target.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Handles.color = _gizmoColor;

        Handles.DrawWireArc(transform.position, transform.up, transform.forward, _viewAngle, _viewRadius);
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, -_viewAngle, _viewRadius);

        Vector3 lineA = Quaternion.AngleAxis(_viewAngle, transform.up) * transform.forward;
        Vector3 lineB = Quaternion.AngleAxis(-_viewAngle, transform.up) * transform.forward;
        Handles.DrawLine(transform.position, transform.position + (lineA * _viewRadius));                   // left and right vectors are normalized vectors( of unit 1)
        Handles.DrawLine(transform.position, transform.position + (lineB * _viewRadius));    
    }
    
}
