using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class AddTorque : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable to gun "+name);
        _grabInteractable.selectExited.AddListener(ApplyTorque);
    }
    
    private void ApplyTorque(SelectExitEventArgs arg0)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 10f);
        GetComponent<Rigidbody>().AddTorque(transform.right * 50000);
    }
    
}
