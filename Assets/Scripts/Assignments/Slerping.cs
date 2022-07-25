using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slerping : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private float _journeyTime = 1.0f;
    [SerializeField] private float _speed = 1.0f;

    private float _startTime;
    private Vector3 _centerPoint;
    private Vector3 _startRelCenter;
    private Vector3 _endRelCenter;
    

    // Update is called once per frame
    void Update()
    {
        GetCenter(Vector3.down);

        float fracComplete = Mathf.PingPong(Time.time - _startTime, _journeyTime/ _speed);
        transform.position = Vector3.Slerp(_startRelCenter, _endRelCenter, fracComplete * _speed);
        transform.rotation = Quaternion.Slerp(_startPos.rotation, _endPos.rotation, fracComplete * _speed);
        transform.position += _centerPoint;
        if (fracComplete >= 1)
        {
            _startTime = Time.time;
        }
        
    }

    private void GetCenter(Vector3 direction)
    {
        _centerPoint = (_startPos.position + _endPos.position) * .5f;
        _centerPoint -= direction;
        _startRelCenter = _startPos.position - _centerPoint;
        _endRelCenter = _endPos.position - _centerPoint;
    }
}
