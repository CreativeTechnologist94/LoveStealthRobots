using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
   
    private Vector3 _interactorPosition = Vector3.zero;
    private Quaternion _interactorRotation = Quaternion.identity;

  
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        StoreInteractor(args);
        MatchAttachmentPoints(args);
    }


    private void StoreInteractor(SelectEnterEventArgs args)
    {
      
        _interactorPosition = args.interactorObject.GetAttachTransform(args.interactableObject).localPosition;
        _interactorRotation = args.interactorObject.GetAttachTransform(args.interactableObject).localRotation;
    }

    private void MatchAttachmentPoints(SelectEnterEventArgs args)
    {
        
        bool hasAttach = attachTransform != null;
        args.interactorObject.GetAttachTransform(args.interactableObject).position = hasAttach ? attachTransform.position : transform.position;
        args.interactorObject.GetAttachTransform(args.interactableObject).rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        ResetAttachmentpoits(args);
        ClearInteractor(args);
    }

    private void ResetAttachmentpoits(SelectExitEventArgs args)
    {
        
        args.interactorObject.GetAttachTransform(args.interactableObject).localPosition = _interactorPosition;
        args.interactorObject.GetAttachTransform(args.interactableObject).localRotation = _interactorRotation;
    }

    private void ClearInteractor(SelectExitEventArgs args)
    {
        _interactorPosition = Vector3.zero;
        _interactorRotation = Quaternion.identity;
    }
    
}