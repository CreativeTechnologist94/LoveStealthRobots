using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GameObject() == player)                   
        {
            Debug.Log("Player entered the goal.");
        }
    }
}
