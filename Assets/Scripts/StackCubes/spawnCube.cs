using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class spawnCube : MonoBehaviour
{
    [SerializeField] private InputActionReference _spawnActionReference;
    [SerializeField] private InputActionReference _LeftGripHoldActionReference;
    [SerializeField] private InputActionReference _RightGripHoldActionReference;
    [SerializeField] private InputActionReference _LeftGripReleaseActionReference;
    [SerializeField] private InputActionReference _RightGripReleaseActionReference;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private XRBaseControllerInteractor _RHController;
    [SerializeField] private XRBaseControllerInteractor _LHController;

    private GameObject _spawnedCube;
    public bool _isLeftPressed = false;
    public bool _isRightPressed = false;
    public bool _isLeftReleased = false;
    public bool _isRightReleased = false;
    private bool _check = false;
    private bool _cubeActive = false;
    private float initialGrabDistance;
    private float currentGrabDistance;


    // Start is called before the first frame update
    void Start()
    {
        _spawnActionReference.action.performed += OnSpawn;
        _LeftGripHoldActionReference.action.performed += OnLeftPress;
        _RightGripHoldActionReference.action.performed += OnRightPress;
        _LeftGripReleaseActionReference.action.performed += OnLeftRelease;
        _RightGripReleaseActionReference.action.performed += OnRightRelease;
    }

    private void OnRightPress(InputAction.CallbackContext obj)
    {
        _isRightReleased = false;
        _isRightPressed = true;
    }

    private void OnLeftPress(InputAction.CallbackContext obj)
    {
        _isLeftReleased =false;
        _isLeftPressed = true;
    }

    private void OnRightRelease(InputAction.CallbackContext obj)
    {
        _isRightPressed = false;
        _isRightReleased = true;
        if(_cubeActive) ReleaseCube();
    }

    private void OnLeftRelease(InputAction.CallbackContext obj)
    {
        _isLeftPressed = false;
        _isLeftReleased = true;
        if(_cubeActive) ReleaseCube();
    }
    
    private void OnSpawn(InputAction.CallbackContext obj)
    {
        if (_cubeActive) return;
        if (_isLeftPressed && _isRightPressed)
        {
            _check = true;
            initialGrabDistance = Vector3.Distance(_LHController.transform.position, _RHController.transform.position);
            _cubeActive = true;
            Vector3 spawnPosition = _RHController.transform.position + (Vector3.back * 0.1f) + (Vector3.left * 0.15f);
            _spawnedCube = Instantiate(_cubePrefab,  spawnPosition, _RHController.transform.rotation);
        }
    }

    private void Update()
    {
        if(!_check) return;
        if (!_cubeActive) return;
        
        _spawnedCube.transform.position = _RHController.transform.position + (Vector3.back * 0.1f) + (Vector3.left * 0.15f);;
        _spawnedCube.transform.rotation = _RHController.transform.rotation;

        ScaleCube();
    }

    private void ReleaseCube()
    {
        if (!_cubeActive) return;
        _spawnedCube.transform.GetComponent<Rigidbody>().isKinematic = false;
        _cubeActive = false;
    }

    private void ScaleCube()
    {
        if (!_cubeActive) return;
        
        if (_isLeftPressed && _isRightPressed)
        {
            Vector3 initialObjectScale = _spawnedCube.transform.localScale;
            currentGrabDistance = Vector3.Distance(_LHController.transform.position, _RHController.transform.position);
            float p = currentGrabDistance / initialGrabDistance * 0.05f;
            Vector3 newScale = new Vector3(Mathf.Clamp((p * initialObjectScale.x), 1.0f, 1.5f), Mathf.Clamp((p * initialObjectScale.y), 1.0f, 1.5f), Mathf.Clamp((p * initialObjectScale.z), 1.0f, 1.5f));
            _spawnedCube.transform.localScale = newScale;
        }
        
    }
}
