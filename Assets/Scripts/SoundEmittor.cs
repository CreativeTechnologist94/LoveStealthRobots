using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmittor : MonoBehaviour
{
    [SerializeField] private float _soundRadius = 5f;
    [SerializeField] private float _impulseThreshold = 1f;
    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.impulse.magnitude > _impulseThreshold || other.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
            
            Debug.Log("Sound Emittor Collided with"+ other.gameObject.name);
            Collider[] _colliders= Physics.OverlapSphere(transform.position, _soundRadius);                                     //can also define as var for shorthand and is used to return any data type
            foreach (var col in _colliders)
            {
                if (col.TryGetComponent(out EnemyController enemyController))
                {
                    enemyController.InvestigatePoint(transform.position);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _soundRadius);
    }
}
