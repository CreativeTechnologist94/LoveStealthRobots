using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomMultiInteractable : XRBaseInteractable
{
    private MaterialApplier _materialApplier;

    protected override void Awake()
    {
        base.Awake();
        _materialApplier = GetComponent<MaterialApplier>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (HasMultipleInteractors())
            _materialApplier.ApplyOther();
    }

    private bool HasMultipleInteractors()
    {
        return interactorsSelecting.Count > 1;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        if(HasNoInteractors())
            _materialApplier.ApplyOriginal();
    }

    private bool HasNoInteractors()
    {
        return interactorsSelecting.Count == 0;
    }

    void Start()
    {
        IXRSelectInteractor newInteractor = firstInteractorSelecting;

        List<IXRSelectInteractor> moreInteractors = interactorsSelecting;

    }
    
}
