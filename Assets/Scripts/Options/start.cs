using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;
    private void OnTriggerEnter(Collider other)
    {
        _gameManager.GetComponent<CubeStack>().OnStart();
    }
}
