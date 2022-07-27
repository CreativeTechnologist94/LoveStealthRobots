using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTeleportationManager : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private InputActionReference _translateGripPressReference;
    [SerializeField] private InputActionReference _translateGripReleaseReference;
    [SerializeField] private XRRayInteractor _rayInteractor;
    //[SerializeField] private Volume _volume;

    private bool _isActive = false;
    private RaycastHit _hit;
    private Vector3 _destinationPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _rayInteractor.enabled = false;
        
        _translateGripPressReference.action.performed += OnTranslate;
        _translateGripReleaseReference.action.performed += OnTranslateCancel;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_isActive) return;
        
        _destinationPosition = _hit.point;
        StartCoroutine(LerpPosition(_destinationPosition, 5f));

        if (transform.position == _destinationPosition)
        {
            _isActive = false;
            _volume.enabled = false;
        }
    }
    
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
    
    private void OnTranslate(InputAction.CallbackContext obj)
    {
        _rayInteractor.enabled = true;
    }
    
    private void OnTranslateCancel(InputAction.CallbackContext obj)
    {
        if (!_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            _rayInteractor.enabled = false;
            return;
        }
        if (hit.transform.gameObject.CompareTag("Floor"))
        {
            _hit = hit;
            _isActive = true;
            _volume.enabled = true;
        }
        _rayInteractor.enabled = false;
    }
}

