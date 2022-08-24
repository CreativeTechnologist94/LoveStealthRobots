using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class spawnCube : MonoBehaviour
{
    [SerializeField] private InputActionReference _SpawnPressActionReference;
    [SerializeField] private InputActionReference _SpawnReleaseActionReference;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private XRBaseControllerInteractor _RHController;
    [SerializeField] private XRBaseControllerInteractor _LHController;
    
   
    
    public bool _isRightPressed = false;
    public bool _isRightReleased = false;

    public float totalVolume = 0f;
    
    private float currentDistance;
    private float spawnDistance;
    private Vector3 initialObjectScale;
    
    private GameObject _spawnedCube;

    // Start is called before the first frame update
    void Start()
    {
        _SpawnPressActionReference.action.performed += OnRightPress;
        _SpawnReleaseActionReference.action.performed += OnRightRelease;
    }
    
    private void Update()
    {
        if (_isRightPressed)
        {
            _spawnedCube.transform.position = Vector3.Lerp(_LHController.transform.position, _RHController.transform.position, 0.5f);
            ScaleCube();
            Debug.Log(_spawnedCube.transform.localScale);
        }
    }
    
    private void OnRightPress(InputAction.CallbackContext obj)
    {
        _isRightReleased = false;
        IntantiateCube();
        _isRightPressed = true;
    }
    
    private void OnRightRelease(InputAction.CallbackContext obj)
    {
        _isRightPressed = false;
        _isRightReleased = true;
        ReleaseCube();
    }
    
    private void IntantiateCube()
    {
        spawnDistance =  Vector3.Distance(_LHController.transform.position, _RHController.transform.position);
        
        Vector3 spawnPosition = Vector3.Lerp(_LHController.transform.position, _RHController.transform.position, 0.5f) + (Vector3.back * 0.1f);
        _spawnedCube = Instantiate(_cubePrefab,  spawnPosition, quaternion.identity);

        initialObjectScale = _spawnedCube.transform.localScale;

    }
    private void ScaleCube()
    {
        currentDistance = Vector3.Distance(_LHController.transform.position, _RHController.transform.position);
        
        float p = currentDistance / spawnDistance * 2f ;
        //Vector3 newScale = new Vector3(Mathf.Clamp((p * initialObjectScale.x), 0.1f, 10f), Mathf.Clamp((p * initialObjectScale.y), 0.1f, 10f), Mathf.Clamp((p * initialObjectScale.z), 0.1f, 10f));
        Vector3 newScale = new Vector3(p * initialObjectScale.x, p * initialObjectScale.y, p * initialObjectScale.z );
        _spawnedCube.transform.localScale = newScale;

    }
    
    private void ReleaseCube()
    {
        _spawnedCube.transform.GetComponent<Rigidbody>().isKinematic = false;

        float volume = _spawnedCube.transform.localScale.x * _spawnedCube.transform.localScale.y *
                       _spawnedCube.transform.localScale.z;

        totalVolume += volume;
    }

    
}
