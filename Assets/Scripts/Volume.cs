using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Kit;
using UnityEngine;

public class Volume : MonoBehaviour
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _spaceship; 
    
    [SerializeField] Bounds _bounds;
    [SerializeField] private Vector3 _boundsSize;
    
    [SerializeField] private bool handInBounds = false;
    
    private float _scaledValueX = 0;
    private float _scaledValueY = 0;
    private float _scaledValueZ = 0;
    [SerializeField] private float _remapedValueX = 0;
    [SerializeField] private float _remapedValueY = 0;
    [SerializeField] private float _remapedValueZ = 0;
    [SerializeField] private float _actualDistanceFromCenter = 0;

    private Vector3 _last;
    private Vector3 _acceleration;
    private Vector3 _distancemoved = Vector3.zero;
    private Vector3 _lastdistancemoved = Vector3.zero;
    

    private void Start()
    {
        _bounds = new Bounds(transform.position, _boundsSize);
        _last= _hand.transform.position;
    }

    private void Update()
    {
        _actualDistanceFromCenter = Vector3.Distance(transform.position, _hand.transform.position);
        CheckBounds();
        
        if (handInBounds)
        {
            UpdateScaledValues();

            //_spaceship.transform.position += new Vector3(_remapedValueX, _remapedValueY, _remapedValueZ)*Time.deltaTime;

            _distancemoved = (_hand.transform.position - _last);
            _acceleration = (_distancemoved - _lastdistancemoved);
            _lastdistancemoved = _distancemoved;
            _last = transform.position;

            _spaceship.transform.position += _acceleration ; //TRY ACCELERATION
            
            _spaceship.transform.rotation = _hand.transform.rotation;
            

        }
        
    }

    private void CheckBounds()
    {
        Vector3 handPosition = _hand.transform.position;
        handInBounds = _bounds.Contains(handPosition) ? true : false;
    }
    private void OnDrawGizmos()
    {
        GizmosExtend.DrawBounds(_bounds, Color.magenta);
    }
    
    private void UpdateScaledValues()
    {
       
        _scaledValueX = GetScaledValue(_hand.transform.position.x, -_bounds.extents.x, _bounds.extents.x, transform.position.x);
        _remapedValueX = (2 * _scaledValueX) - 1;

        _scaledValueZ = GetScaledValue(_hand.transform.position.z, -_bounds.extents.z, _bounds.extents.z, transform.position.z);  
        _remapedValueZ = (2 * _scaledValueZ) - 1;

        _scaledValueY = GetScaledValue(_hand.transform.position.y, -_bounds.extents.y, _bounds.extents.y, transform.position.y);
        _remapedValueY = (2 * _scaledValueY) - 1;
        
    }

    private float GetScaledValue(float rawValue, float minValue, float maxValue, float centerWorldSpacePos)
    {
        float returnScaledValue = 0f;

        minValue += centerWorldSpacePos;
        maxValue += centerWorldSpacePos;

        if (minValue == 0f)
        {
            returnScaledValue = rawValue / maxValue;
        }
        else
        {
            returnScaledValue = (rawValue - minValue) / (maxValue - minValue);
        }
        return returnScaledValue;
    }
}
