using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using UnityEngine;

public class CustomGravity1 : MonoBehaviour
{
    [SerializeField] private float multiplier = 1;
    [SerializeField] private Transform _gravityPos;
    private Body[] _bodies;
    private float _g;
    private GravityManipulation _gravityManipulation;
    private float _mass;

    // Start is called before the first frame update
    private void Start()
    {
        _g = 6.67f * Mathf.Pow(10, -11);
        _mass = GetComponent<Rigidbody>().mass;
        _bodies = FindObjectsOfType<Body>();
        _gravityManipulation = GetComponent<GravityManipulation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var item in _bodies)
        {
            if (_gravityManipulation.pullActive || _gravityManipulation.pushActive)
            {
                item.rigidBody.useGravity = false;
                Vector3 force = CalculateForce(item);
                ApplyForce(item, force);
            }
            else
            {
                item.rigidBody.useGravity = true;
            }
        }
    }

    private Vector3 CalculateForce(Body body)
    {
        float distance = Vector3.Distance(_gravityPos.position, body.transform.position);
        float distanceSqr = distance * distance;
        float force = _g * _mass * body.mass / distanceSqr;
        force *= multiplier;
        return force * (_gravityPos.position - body.transform.position).normalized;
    }
    
    private void ApplyForce(Body body, Vector3 force)
    {
        if(_gravityManipulation.plungerType == PlungerType.Pull)
        {
            Vector3 acceleration = force / body.mass;
            body.rigidBody.AddForce(acceleration, ForceMode.Acceleration);
        }
        else if(_gravityManipulation.plungerType == PlungerType.Push)
        {
            Vector3 acceleration = force / body.mass;
            body.rigidBody.AddForce(-acceleration, ForceMode.Acceleration);
        }
    }
    
}
