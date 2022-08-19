using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [HideInInspector] public float mass;
    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public MeshRenderer bodyRenderer;
    public bool relocated;
    public bool exploded;
    
    // Start is called before the first frame update
    void Start()
    {
        mass = GetComponent<Rigidbody>().mass;
        rigidBody = GetComponent<Rigidbody>();
        bodyRenderer = GetComponent<MeshRenderer>();
        relocated = false;
        exploded = false;
    }

}
