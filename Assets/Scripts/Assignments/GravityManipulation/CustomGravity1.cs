using System.Numerics;
using System.Windows.Forms.VisualStyles;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CustomGravity1 : MonoBehaviour
{
    [SerializeField] private GameObject _leftPlunger;
    [SerializeField] private GameObject _rightPlunger;
    [SerializeField] private Transform _leftgravityPos;
    [SerializeField] private Transform _rightgravityPos;
    [SerializeField] private float multiplier = 0.02f; //75
    
    private Body[] _bodies;
    private GravityManipulation _leftgravityManipulation;
    private GravityManipulation _rightgravityManipulation;
    private Vector3 _lastpos ;
    private Vector3 centerPos;

    public bool pushactive;
    public bool pullactive;
    public bool bothactive;

    // Start is called before the first frame update
    private void Start()
    {
        _bodies = FindObjectsOfType<Body>();
        _leftgravityManipulation = _leftPlunger.GetComponent<GravityManipulation>();
        _rightgravityManipulation = _rightPlunger.GetComponent<GravityManipulation>();
        
        _lastpos = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var item in _bodies)
        {
            if ( _leftgravityManipulation.pushActive && !_rightgravityManipulation.pullActive)
            {
                pushactive = false;
                pullactive = true;
                bothactive = false;
                
                Vector3 dir = (item.transform.position - _leftgravityPos.position).normalized;
                item.rigidBody.velocity = dir * (multiplier * Time.fixedDeltaTime);
                //Vector3 force = (item.transform.position - _leftgravityPos.position).normalized * (Time.fixedDeltaTime * multiplier);
                //item.rigidBody.AddForce(force, ForceMode.Force);
            }
            else if (_rightgravityManipulation.pullActive && !_leftgravityManipulation.pushActive)
            {
                pullactive = true;
                pushactive = false;
                bothactive = false;
                
                Vector3 dir = (_rightgravityPos.position - item.transform.position).normalized;
                item.rigidBody.velocity = dir * (multiplier * Time.fixedDeltaTime);
                //Vector3 force = (_rightgravityPos.position - item.transform.position).normalized * (Time.fixedDeltaTime * multiplier);
                //item.rigidBody.AddForce(force, ForceMode.Force);
            }
            else if (_rightgravityManipulation.pullActive && _leftgravityManipulation.pushActive)
            {
                pullactive = false;
                pushactive = false;
                bothactive = true;
                
                centerPos = (_rightgravityPos.position + _leftgravityPos.position) / 2;
                Vector3 dir = (centerPos - _lastpos).normalized;
                item.rigidBody.velocity = dir * (multiplier * Time.fixedDeltaTime);
                
            }
            else 
            {
                pushactive = false;
                pullactive = false;
                bothactive = false;
            }
        }
        _lastpos = centerPos;
    }
    
   
}
