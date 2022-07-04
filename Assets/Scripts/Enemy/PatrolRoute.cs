using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    public enum PatrolType
    {
        Loop = 0,
        PingPong = 1
    }

    [SerializeField] private Color _patrolRouteColor = Color.green;
    public PatrolType patrolType;                                //enum becomes a data type
    [SerializeField] public List<Transform> route;

    [Button("Add Patrol Point")]                                              //odin attribute
    private void AddPatrolPoint()
    {
        GameObject thisPoint = new GameObject();                                   // new instance of the class GameObject (which is not a monobehavior)
        thisPoint.transform.position = transform.position;
        thisPoint.transform.parent = transform;                                    //script is attached to PatrolRoutes Gameobject
        thisPoint.name = "Point" + (route.Count + 1);
        
        #if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(thisPoint, "Add Patrol Point");
        #endif

        route.Add(thisPoint.transform);
    }

    [Button("Reverse Patrol Route")]
    private void ReversePatrolRoute()
    {
        route.Reverse();
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
            Handles.Label(transform.position, gameObject.name);                //add a label outside of an editor script
        #endif
        
        Gizmos.color = _patrolRouteColor;                                          //add color

        for (int i = 0; i < route.Count-1; i++)
        {
            Gizmos.DrawLine(route[i].position, route[i+1].position);
        }

        if (patrolType == PatrolType.Loop)
        {
            Gizmos.DrawLine(route[route.Count-1].position, route[0].position);
        }
    }
    
}
