using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class target : MonoBehaviour
{
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    public bool hasFallen = false;
    [SerializeField] private Transform _moveposition;
    private float _speed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0,2);
        if (rand == 0)
        {
            _moveposition = _leftPoint;
        }
        else
        {
            _moveposition = _rightPoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFallen)
        {
            MoveTarget();
        }
        
    }
    public void FallDown()
    {
        if (hasFallen == true)
        {
            transform.Rotate(0,-90,0);
        }
    }

    public void StandBackUp()
    {
        if (hasFallen != true) return;
        hasFallen = false;
        transform.Rotate(0,90,0);
    }

    private void MoveTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _moveposition.position, _speed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, _moveposition.position);
        if (dist != 0f) return;
        if (_moveposition == _leftPoint)
        {
            _moveposition = _rightPoint;
        }
        else
        {
            _moveposition = _leftPoint;
        }
    }
}
