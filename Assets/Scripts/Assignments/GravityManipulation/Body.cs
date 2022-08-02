using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [HideInInspector] public float mass;
    [HideInInspector] public Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        mass = GetComponent<Rigidbody>().mass;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
