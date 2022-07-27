using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTeleportationProvider : TeleportationProvider
{ 
        protected override void Update()
        {
            if (!validRequest || !BeginLocomotion())
                return;

            var xrOrigin = system.xrOrigin;
            if (xrOrigin != null)
            {
                switch (currentRequest.matchOrientation)
                {
                    case MatchOrientation.WorldSpaceUp:
                        xrOrigin.MatchOriginUp(Vector3.up);
                        break;
                    case MatchOrientation.TargetUp:
                        xrOrigin.MatchOriginUp(currentRequest.destinationRotation * Vector3.up);
                        break;
                    case MatchOrientation.TargetUpAndForward:
                        xrOrigin.MatchOriginUpCameraForward(currentRequest.destinationRotation * Vector3.up, currentRequest.destinationRotation * Vector3.forward);
                        break;
                    case MatchOrientation.None:
                        // Change nothing. Maintain current origin rotation.
                        break;
                    default:
                        Assert.IsTrue(false, $"Unhandled {nameof(MatchOrientation)}={currentRequest.matchOrientation}.");
                        break;
                }

                var heightAdjustment = xrOrigin.Origin.transform.up * xrOrigin.CameraInOriginSpaceHeight;

                var cameraDestination = currentRequest.destinationPosition + heightAdjustment;
                
                /*
                while (xrOrigin.Origin.transform.position != currentRequest.destinationPosition)
                {
                    Debug.Log("While loop working.");
                    
                    var direction = (currentRequest.destinationPosition - xrOrigin.Origin.transform.position).normalized;
                    xrOrigin.Origin.transform.Translate(direction);
                }

                //xrOrigin.MoveCameraToWorldLocation(cameraDestination);
                 */ 
                
                xrOrigin.MoveCameraToWorldLocation(cameraDestination);
            }
            
            EndLocomotion();
            validRequest = false;
        }
}


