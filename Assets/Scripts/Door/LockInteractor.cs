using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class LockInteractor : MonoBehaviour
{
    [SerializeField] private GameObject _assignedDoor;

    private Key _key;
    private Key _lock;

    // Start is called before the first frame update
    void Start()
    {
        _lock = transform.GetComponent<Key>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Obstacles"))
            {
                _key = other.transform.GetComponent<Key>();
                if (_key._keyType == _lock._keyType)
                {
                    Debug.Log("correct key inserted is " + _key._keyType.ToString());
                    other.transform.GetComponent<Rigidbody>().isKinematic = true;
                    other.transform.position = transform.position;
                    other.transform.eulerAngles = new Vector3(-90f, 0f, 0f);

                    _assignedDoor.GetComponent<DoorInteractor>().keyinserted = true;
                    Debug.Log("Switched keyinserted state to true");
                }
            }
        }
    }
}