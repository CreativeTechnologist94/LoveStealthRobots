using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTeleport : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _teleportController;
    
    // Start is called before the first frame update
    void Start()
    {
        _teleportController.selectEntered.AddListener(SmoothTransition);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SmoothTransition(SelectEnterEventArgs arg0)
    {
        throw new System.NotImplementedException();
    }

}
