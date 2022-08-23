using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffInteraction : MonoBehaviour
{
    public Material[] _stuffMaterials = new Material[4];
    [SerializeField] private Light _pointlight;
    private int _currentIndex;
    private List<Transform> _objects = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        _currentIndex = 0;
        
        foreach(Transform child in transform)
        {
            _objects.Add(child);
        }
    }

    public void SwitchOnOff()
    {
        if (_pointlight.gameObject.GetComponent<Light>().enabled == true)
        {
            _pointlight.gameObject.GetComponent<Light>().enabled = false;
        }
        else
        {
            _pointlight.gameObject.GetComponent<Light>().enabled = true;
        }
    }

    public void MakeLightRed()
    {
        if (_pointlight.gameObject.GetComponent<Light>().enabled == true) _pointlight.gameObject.GetComponent<Light>().color = Color.red;
    }
    public void MakeLightGreen()
    {
        if (_pointlight.gameObject.GetComponent<Light>().enabled == true) _pointlight.gameObject.GetComponent<Light>().color = Color.green;
    }
    public void MakeLightYellow()
    {
        if (_pointlight.gameObject.GetComponent<Light>().enabled == true) _pointlight.gameObject.GetComponent<Light>().color = Color.yellow;
    }
    
    public void ChangeStuffMaterial()
    {
        if (_currentIndex == 0)
        {
            foreach (var obj in _objects)
            { 
                obj.gameObject.GetComponent<Renderer>().material = _stuffMaterials[1];
            }
            _currentIndex = 1;
        }
        else if (_currentIndex == 1)
        {
            foreach (var obj in _objects)
            {
                obj.gameObject.GetComponent<Renderer>().material = _stuffMaterials[2];
            }
            _currentIndex = 2;
        }
        else if (_currentIndex == 2)
        {
            foreach (var obj in _objects)
            {
                obj.gameObject.GetComponent<Renderer>().material = _stuffMaterials[3];
            }
            _currentIndex = 3;
        }
        else if (_currentIndex == 3)
        {
            foreach (var obj in _objects)
            {
                obj.GetComponent<Renderer>().material = _stuffMaterials[0];
            }
            _currentIndex = 0;
        }
    }

    public void ChangeGravity()
    {
        foreach (var obj in _objects)
        { 
            var _rigidbody =  obj.gameObject.GetComponent<Rigidbody>();
            if (_rigidbody.useGravity == false)
            {
                _rigidbody.useGravity = true;
            }
            else
            {
                _rigidbody.useGravity = false;
            }
        }
    }
    
}
