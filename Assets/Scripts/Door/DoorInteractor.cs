using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractor : MonoBehaviour
{
    public bool keyinserted ;
    private Animator _myDoor = null;

    private void Start()
    {
        keyinserted = false;
        _myDoor = transform.parent.GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (keyinserted)
        {
            Debug.Log("opening door");
            //_myDoor.Play("DoorOpen", 0, 0f);
            _myDoor.SetBool("open", true);
        }

        keyinserted = false;
    }
    
}