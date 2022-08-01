using System;
using System.Windows.Forms.VisualStyles;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class WallStickReturn : MonoBehaviour
{
    [SerializeField] private Collider _sharpEnd;
    [SerializeField] private InputActionReference _recallReference;
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private float _torqueForce = 50000f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private bool _return = false;
    private Rigidbody _rb;
    private float distance;
    void Start()
    {
        Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable to gun "+name);
        
        _rb = GetComponent<Rigidbody>();
        
        _grabInteractable.selectExited.AddListener(ApplyTorque);

        _recallReference.action.performed += OnRecall;
    }
    private void Update()
    {
        if (_return != true) return;
        
        distance = Vector3.Distance(_handPosition.position, transform.position);
        if (distance > 0.1f)
        {
            var dir = (_handPosition.position - transform.position).normalized;
            _rb.AddForce(dir * (_force * 1.5f));
            _rb.AddTorque(transform.right * (_torqueForce*1.5f));
        }
        else 
        {
            _return = false;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.position = _handPosition.position;
        }
    }
    
    private void ApplyTorque(SelectExitEventArgs arg0)
    {
        _rb.isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * _force);
        GetComponent<Rigidbody>().AddTorque(transform.right * _torqueForce);
    }
    
    private void OnRecall(InputAction.CallbackContext obj)
    {
        Debug.Log("Recall");
        _rb.isKinematic = false;
        _return = true;
    }

    private void OnCollisionStay(Collision other)
    {
        foreach (ContactPoint contactPoint in other.contacts)
        {
            if (contactPoint.thisCollider != _sharpEnd) continue;
            
            if (other.gameObject.TryGetComponent(out EnemeyController _enemy))
            {
                _rb.isKinematic = true;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
            {
                _rb.isKinematic = true;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _rb.isKinematic = false;
    }
    
    
}
