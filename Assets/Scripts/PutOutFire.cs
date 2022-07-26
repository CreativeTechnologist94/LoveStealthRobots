using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PutOutFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Water colliding with: " + other.gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                //Debug.Log(child.name);
                if (child.name == "Fire")
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
    
}
