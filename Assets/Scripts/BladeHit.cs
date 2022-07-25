using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BladeHit : MonoBehaviour
{
    
    public UnityEvent OnBladeHit;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.TryGetComponent(out Creature targetCreature))
        {
            targetCreature = other.gameObject.GetComponentInParent<Creature>();
            if(!targetCreature) return;
        }
        Debug.Log("Blade has hit: "+ other.gameObject);
        _audioSource.Play();
        OnBladeHit.Invoke();
    }
}
