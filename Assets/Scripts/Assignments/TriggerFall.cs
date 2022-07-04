using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _fallingcube;
    [SerializeField] [Tooltip("In Seconds")] private float startTime = 5.0f;
    private float elapsed = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.GameObject() == _player)
        {
            elapsed +=  Time.fixedDeltaTime;
            if (elapsed > startTime)
            {
                _fallingcube.SetActive(true);
            }
        }
    }
}
