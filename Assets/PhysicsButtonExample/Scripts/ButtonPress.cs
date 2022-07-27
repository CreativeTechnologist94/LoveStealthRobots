using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonPress : MonoBehaviour
{
    [SerializeField] private GameObject ButtonTop;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerSet1;
    [SerializeField] private LayerMask _layerSet2;
    [SerializeField] private bool _buttonPressed = false;
    
    // Setup References and listener for changing button color
    void Start()
    {
        _camera.cullingMask = _layerSet1;
    }

    private void OnTriggerEnter(Collider other)
    {
        _buttonPressed = true;
        if(other.CompareTag("Button"))   //We only want to fire if we were triggered by our button, so we check the tag
        {
            if (_camera.cullingMask == _layerSet1)
            {
                _camera.cullingMask = _layerSet2;
            }
            else
            {
                _camera.cullingMask = _layerSet1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _buttonPressed = false;
    }
}
