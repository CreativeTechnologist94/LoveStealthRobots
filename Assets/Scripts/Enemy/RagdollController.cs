using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;
    private EnemyController roboController;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();

        foreach (Rigidbody rgb in _rigidbodies)
        {
            rgb.isKinematic = true;
        }

        foreach (Collider col in _colliders)
        {
            col.enabled = false;
        }
    }

    [Button]
    public void EnableRagdoll()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponent<FieldOfView>().enabled = false;

        foreach (Rigidbody rgb in _rigidbodies)
        {
            rgb.isKinematic = false;
        }

        foreach (Collider col in _colliders)
        {
            col.enabled = true;
        }
        
    }
}
