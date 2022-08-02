using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float multiplier = 1;
    private CustomGravity[] bodies;
    private Collider col;
    private float G;
    [HideInInspector] public float mass;
    [HideInInspector] public Rigidbody rb;
    Vector3 velocity;
    
    // Start is called before the first frame update
    private void Start()
    {
        G = 6.67f * Mathf.Pow(10, -11);
        mass = GetComponent<Rigidbody>().mass;
        bodies = FindObjectsOfType<CustomGravity>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var item in bodies)
        {
            if (item == this) return;
            Vector3 force = CalculateForce(item);
            ApplyForce(item, force);
        }
    }

    private Vector3 CalculateForce(CustomGravity body)
    {
        float distance = Vector3.Distance(transform.position, body.transform.position);
        float distanceSqr = distance * distance;
        float force = G * mass * body.mass / distanceSqr;
        force *= multiplier;
        return force * (transform.position - body.transform.position).normalized;
    }
    
    private void ApplyForce(CustomGravity body, Vector3 force)
    {
        Vector3 acceleration = force / body.mass;
        body.rb.AddForce(acceleration, ForceMode.Acceleration);
    }

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}
