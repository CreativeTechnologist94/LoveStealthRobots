using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform _pickPosition;
    [SerializeField] private float _pickRange = 2f;
    
    private Rigidbody _pickedObject;
    private bool boxOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPick()
    {
        if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward, out RaycastHit hit, _pickRange)) 
        {
            if (!hit.transform.gameObject.CompareTag("Wearable")) return;                                                   

            _pickedObject = hit.transform.GetComponent<Rigidbody>();
            _pickedObject.isKinematic = true;
            
            _pickedObject.transform.position = _pickPosition.position;
            //_pickedObject.transform.rotation = Quaternion.LookRotation(_cameraPosition.forward, Vector3.up);
            _pickedObject.transform.LookAt(_pickedObject.transform.position + _cameraPosition.up.normalized);
            _pickedObject.transform.parent = _pickPosition;

        }
    }
    
}
