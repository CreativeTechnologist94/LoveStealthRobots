using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 _spawnPos;
    public bool ballHit = false;
    private MeshRenderer _meshRenderer; 
    void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _spawnPos = transform.position;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Resetter"))
        {
            ballHit = true;
            RemoveBall();
        }
    }
    */
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Resetter"))
        {
            ballHit = true;
            RemoveBall();
        }
    }
    
    private void RemoveBall()
    {
        _meshRenderer.enabled = false;
    }
    
    public void RepositionBall()
    {
        ballHit = false;
        transform.position = _spawnPos;
        _meshRenderer.enabled = true;
       
    }
    
}
