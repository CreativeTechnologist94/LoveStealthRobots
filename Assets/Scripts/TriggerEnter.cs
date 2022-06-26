using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TriggerEnter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _minimap;
    [SerializeField] private GameObject particlefx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GameObject() == player)                   
        {
            Debug.Log("Player entered the goal.");
            _canvas.SetActive(true);
            particlefx.SetActive(true);
            _minimap.SetActive(false);
            player.gameObject.GetComponent<FirstPersonController>().enabled = false;
        }
    }
}
