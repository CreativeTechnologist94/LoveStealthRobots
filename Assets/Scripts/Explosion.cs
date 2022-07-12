using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _proximityRange = 5f;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _explodingRobot; //prefab
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private GameObject _robotHit;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _proximityRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _robotHit = hit.transform.gameObject;
                Destroy(_robotHit);
                RobotExplosion();
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

    private void RobotExplosion()
    {
        Instantiate(_explodingRobot, _robotHit.transform.position, Quaternion.identity);
        foreach (Transform child in _explodingRobot.transform)
        {
            if (child.TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.AddExplosionForce(_explosionForce, _robotHit.transform.position, _explosionRadius);
            }
        }
    }
    
}
