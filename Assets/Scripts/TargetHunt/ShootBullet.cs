using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _barrelLocation;
    [SerializeField] private float _shotPower = 500f;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _firesound;
    [SerializeField] private ParticleSystem _bonusParticleSystem;
    
    [SerializeField] private GameObject _gameManager;
    
    public void Shoot()
    {
        RaycastHit hit;
        _source.PlayOneShot(_firesound);
        //Instantiate(_bulletPrefab, _barrelLocation.position , _barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(_barrelLocation.forward * _shotPower);
        var instantiatedBullet = Instantiate(_bulletPrefab, _barrelLocation.position, _barrelLocation.rotation);
        instantiatedBullet.GetComponent<Rigidbody>().AddForce(_barrelLocation.forward * _shotPower); //forward
        Destroy(instantiatedBullet, 0.1f);
        
        if (Physics.Raycast(_barrelLocation.position, _barrelLocation.forward , out hit))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit " + hit.transform.gameObject.name);
            
            if (hit.transform.gameObject.CompareTag("target"))
            {
                Debug.Log("Did Hit target : point +1");
                _gameManager.GetComponent<TargetGameManager>().score++;
                /*
                if (hit.transform.gameObject.CompareTag("bonus"))
                {
                    Debug.Log("Did Hit bonus mark : point +2");
                    _bonusParticleSystem.Play();
                    _gameManager.GetComponent<TargetGameManager>().score++;
                }
                */
                hit.transform.GetComponent<target>().hasFallen = true;
                hit.transform.GetComponent<target>().FallDown();
            }

            if (hit.transform.gameObject.CompareTag("Resetter"))
            {
                _gameManager.GetComponent<TargetGameManager>().ResetGame();
            }
        }
    }
}
