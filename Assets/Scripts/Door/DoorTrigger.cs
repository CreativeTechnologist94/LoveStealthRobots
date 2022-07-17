using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())                   //if other has the component DoorInteractor
        {
            OpenDoor();
        }
    }

    protected virtual void OnTriggerExit(Collider other)           //will allow to override in a new script
    {
        if (other.GetComponent<DoorInteractor>())                   
        {
            CloseDoor();
        }
    }

    protected void OpenDoor()
    {
        door.SetActive(false);
    }

    protected void CloseDoor()
    {
        door.SetActive(true);
    }
}
