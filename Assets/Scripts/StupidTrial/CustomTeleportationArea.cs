using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomTeleportationArea : TeleportationArea
{
    [SerializeField] private GameObject _xrOrigin;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected override bool GenerateTeleportRequest(IXRInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        if (raycastHit.collider == null)
            return false;
        
        teleportRequest.destinationPosition = raycastHit.point;
        teleportRequest.destinationRotation = transform.rotation;

        return true;
    }

    protected void FixedUpdate()
    {
        var originTransform = _xrOrigin.transform;
        //var direction = (teleportRequest.destinationPosition - _xrOrigin.transform.position).normalized;
    }
}
