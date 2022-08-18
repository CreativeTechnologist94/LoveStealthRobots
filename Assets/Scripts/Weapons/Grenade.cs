using System;
using Sirenix.OdinInspector;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private Bomb _bombPrefab;
        [SerializeField] private XRGrabInteractable _grabInteractable;
        [SerializeField] protected Transform _grenadeBarrel;
        [SerializeField] private float _force = 20;
        [SerializeField] private Projections _projection;

        private bool _showLine = false;
        
        // Start is called before the first frame update
        protected virtual void Start()
        {
            Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable to grenade"+name);
            Assert.IsNotNull(_grenadeBarrel, "You have not assigned a grenade barrell to "+name);
            
            _grabInteractable.selectEntered.AddListener(ShowTrajectory);
            _grabInteractable.selectExited.AddListener(StopTrajectory);
            _grabInteractable.activated.AddListener(Shoot);
        }
        
        private void Update()
        {
            if (_showLine == true)
            {
                _projection.SimulateTrajectory(_bombPrefab, _grenadeBarrel.position, _grenadeBarrel.forward* _force);
            }
        }

        private void ShowTrajectory(SelectEnterEventArgs arg0)
        {
            _showLine = true;
            _projection._line.enabled = true;
        }
        
        private void StopTrajectory(SelectExitEventArgs arg0)
        {
            _showLine = false;
            _projection._line.enabled = false;
        }
        
        [Button]
        protected virtual void Shoot(ActivateEventArgs arg0)
        {
            
            var bomb = Instantiate(_bombPrefab, _grenadeBarrel.position, Quaternion.identity);
            bomb.Init(_grenadeBarrel.forward*_force, false);

        }

    }
}
