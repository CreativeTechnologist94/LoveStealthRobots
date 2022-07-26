using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFire : MonoBehaviour
{
    public List<Transform> burningObjects;
    
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
        if (burningObjects.Contains(other.transform)) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject clone = Instantiate(this.gameObject) as GameObject;
            clone.transform.SetParent(other.transform);
            clone.gameObject.name = transform.gameObject.name;
            
            burningObjects.Add(other.transform);
        }
    }
    
}
