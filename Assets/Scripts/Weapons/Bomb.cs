using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private MeshRenderer _renderer;
    private Body[] _bodies;
    private Vector3 _hitPos;
    
    private bool _isGhost;
    private float _multiplier = 100f; //0.5f
    private int _counter;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _bodies = FindObjectsOfType<Body>();
        _counter = 0;
    }
    
    private void FixedUpdate()
    {
        if (!_isGhost) return;                                                                                                 //check if bomb landed

        foreach (var item in _bodies)
        {
            if (!item.relocated)                                                                                                //if it has not already relocated
            {
                if (Vector3.Distance(_hitPos, item.transform.position) > 0.35f)                                                  //if distance is more from bomb
                {
                    item.rigidBody.velocity = (_hitPos - item.transform.position) * (_multiplier * Time.fixedDeltaTime);                   //add velocity
                }
                else                                                                                                                 //if distance is lesser
                {
                    item. rigidBody.velocity = Vector3.zero;                                                                               //stop velocity , relocate , add to counter
                    item.bodyRenderer.enabled = false;
                    _counter++;
                    MoveBodyRandom(item);
                }
            }
            else                                                                                                                  //if it has relocated
            {
                if (_counter >=_bodies.Length && !item.exploded)                                                                       //if counter has reached length & item has not exploded                                        
                {
                    AddExplosionforce(item);                                                                                                 //explode
                }
            }
        }
    }

    private void AddExplosionforce(Body body)
    {
        body.bodyRenderer.enabled = true;
        body.rigidBody.AddExplosionForce(20f, _hitPos, 6f, 3f);
        body.exploded = true;
    }

    private void MoveBodyRandom(Body body)
    {
        float x = Random.Range(-2f, 2f);
        float y = Random.Range(0.5f, 2.5f);
        float z = Random.Range(-2f, 2f);
        body.transform.position += new Vector3(x, y, z);
        body.relocated = true;
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
            Debug.Log("hit floor");
            ContactPoint contact = other.contacts[0];
            _hitPos = contact.point;
            _renderer.enabled = false;
            _isGhost = true;
            
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(_hitPos, new Vector3(0.2f, 0.2f, 0.2f));
    }
    
}
