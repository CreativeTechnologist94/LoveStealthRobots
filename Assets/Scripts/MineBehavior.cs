using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour
{
    [SerializeField] private float _proximityRange = 5f;
    [SerializeField] private GameObject _explosion;
    
    private RagdollController _rgdController;
    
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _proximityRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                //Destroy(hit.transform.gameObject);
                //Destroy(transform.gameObject);
                _rgdController = hit.transform.gameObject.GetComponent<RagdollController>();
                _rgdController.EnableRagdoll();
                Debug.Log("Rag doll activated");
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    
}