using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private int _levelNumber;
    [SerializeField] private string _levelname;
    private void OnTriggerEnter(Collider other)
    {
        _gameManager.GetComponent<CubeStack>().levelNum = _levelNumber;
        _gameManager.GetComponent<CubeStack>()._levelText.text = "LEVEL: "+ _levelname;
    }
}
