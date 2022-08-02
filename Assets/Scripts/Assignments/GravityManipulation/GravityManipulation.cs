using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms.VisualStyles;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public enum PlungerType          //static enum
{
    Pull,
    Push
}
public class GravityManipulation : MonoBehaviour
{
    public PlungerType plungerType;
    public bool pullActive = false;
    public bool pushActive = false;
    
    [SerializeField] private XRGrabInteractable _grabInteractable;
    

    // Start is called before the first frame update
    private void Start()
    {
        Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable to plunger "+name);
        _grabInteractable.activated.AddListener(ApplyGravity);
        _grabInteractable.deactivated.AddListener(StopGravity);
    }

    private void ApplyGravity(ActivateEventArgs arg0)
    {
        if (plungerType == PlungerType.Pull)
        {
            pullActive = true;
        }
        else if (plungerType == PlungerType.Push)
        {
            pushActive = true;
        }
    }
    private void StopGravity(DeactivateEventArgs arg0)
    {
        if (plungerType == PlungerType.Pull)
        {
            pullActive = false;
        }
        else if (plungerType == PlungerType.Push)
        {
            pushActive = false;
        }
    }

}
