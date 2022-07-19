using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Can : MonoBehaviour
{
    private Vector3 _spawnPosition;
    public bool hasFallen = false;
    //private CanTossGameManager _gameManager;

    private void Start()
    {
        _spawnPosition = transform.position;
    }
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Resetter"))
        {
            hasFallen = true;
        }
    }
    */
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Resetter"))
        {
            hasFallen = true;
        }
    }

    public void RepositionCan()
    {
        hasFallen = false;
        transform.position = _spawnPosition;
    }
}
