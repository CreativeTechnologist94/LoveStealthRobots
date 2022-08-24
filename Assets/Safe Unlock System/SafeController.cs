using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEditor.TerrainTools;
using UnityEngine.InputSystem;

public class SafeController : MonoBehaviour
{
    [SerializeField] private InputActionReference _enterNum;
    
    [SerializeField] private GameObject _safeDoor;
    [SerializeField] private GameObject _dial;
    
    [SerializeField] private TMP_Text _dialNumText;
    [SerializeField] private TMP_Text _enterNumText;
    [SerializeField] private int _firstNum;
    [SerializeField] private int _secondNum;
    [SerializeField] private int _thirdNum;
    
    public bool firstLockActive = false;
    public bool secondLockActive = false;
    public bool thirdLockActive = false;
    public bool safeOpened = false;
    
    private HingeJoint _hingejoint;
    private ConfigurableJoint _configjoint;
    
    private float angle = 0; 
    private int dialValue = 0;
    

    // Start is called before the first frame update
    private void Start()
    {
        firstLockActive = true;
       _enterNum.action.started += EnterButtonPressed;
       _hingejoint = _safeDoor.GetComponent<HingeJoint>();
       _configjoint = _dial.GetComponent<ConfigurableJoint>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        angle = _dial.transform.localRotation.eulerAngles.z;
        dialValue = (Mathf.RoundToInt(angle / 22.5f));
        _dialNumText.text = dialValue.ToString();
        
        if (firstLockActive)
        {
            _enterNumText.text = "Dial First Number";
        }
        else if (secondLockActive)
        {
            _enterNumText.text = "Dial Second Number";
        }
        else if (thirdLockActive)
        {
            _enterNumText.text = "Dial Third Number";
        }
        else if (safeOpened)
        {
            _enterNumText.text = "Safe Unlocked";
            //_dial.transform.SetParent(_safeDoor.transform);
            _configjoint.xMotion = ConfigurableJointMotion.Free;
            _configjoint.yMotion = ConfigurableJointMotion.Free;
            _configjoint.zMotion = ConfigurableJointMotion.Free;
            _configjoint.angularXMotion = ConfigurableJointMotion.Free;
            _configjoint.angularYMotion = ConfigurableJointMotion.Free;
            _configjoint.autoConfigureConnectedAnchor = false;
            //_hingejoint.autoConfigureConnectedAnchor = true;

        }
    }
    
    private void EnterButtonPressed(InputAction.CallbackContext obj)
    {
        if (firstLockActive)
        {
            if (dialValue == _firstNum)
            {
                firstLockActive = false;
                secondLockActive = true;
            }
        }
        else if (secondLockActive)
        {
            if (dialValue == _secondNum)
            {
                secondLockActive = false;
                thirdLockActive = true;
            }
        }
        else if (thirdLockActive)
        {
            if (dialValue == _thirdNum)
            {
                thirdLockActive = false;
                safeOpened = true;
            }
        }
    }
    
}
