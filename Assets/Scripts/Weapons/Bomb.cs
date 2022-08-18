using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private bool _isGhost;
    private Body[] _bodies;
    private bool _suckbodies = false;
    private float _suction = 50f;
    private float _blackholetime = 3f;
    private float timer;
    private Vector3 _hitPos;
    //variable to refernce renderer

    private void Start()
    {
        _bodies = FindObjectsOfType<Body>();
        
    }
    
    void FixedUpdate()
    {
        if (!_suckbodies) return;
        
        foreach (var item in _bodies)
        {
            if (timer < _blackholetime)
            {
                Vector3 dir = (item.transform.position - _hitPos).normalized;
                //item.rigidBody.AddForce(dir * _suction, ForceMode.Force);
                item.transform.position =
                    Vector3.MoveTowards(item.transform.position, _hitPos, _suction * Time.deltaTime);
            }
            else if (timer == _blackholetime)
            {
                item.rigidBody.isKinematic = true;
                item.transform.GetComponent<MeshRenderer>().enabled = false;
                item.transform.position = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) + Vector3.up; ///world space
                
            }
            else if (timer == (_blackholetime + 2))
            {
                item.transform.GetComponent<MeshRenderer>().enabled = true;
                item.rigidBody.isKinematic = false;
                _suckbodies = false;
            }
            
        }
        
        timer += Time.deltaTime;
    }
    public void Init(Vector3 velocity, bool isGhost)
   {
       _isGhost = isGhost;
       _rb.AddForce(velocity, ForceMode.Impulse);
   }
    private void OnCollisionEnter(Collision other)
    {
        if (_isGhost) return;
        
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("first"); 
            Destroy(gameObject);  
            Debug.Log("second");
            ContactPoint contact = other.contacts[0];
            _hitPos = contact.point;
            
            timer = 0f;
            _suckbodies = true;
        }
    }
}
