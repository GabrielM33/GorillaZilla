using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    //[TODO] Refactor this code
    public class PowerUpBuilding : MonoBehaviour
    {
        public TouchHandGrabInteractable grabbable;
        public DestructableBuilding destructableBuilding;
        bool isGrabbed = false;
        // Start is called before the first frame update
        void Start()
        {
            grabbable.GetComponent<TouchHandGrabInteractable>().WhenSelectingInteractorAdded.Action += OnPowerUpGrabbed;
            destructableBuilding.onBuildingHit.AddListener(OnBuildingHit);
        }

        private void OnBuildingHit()
        {
            if (!isGrabbed && grabbable)
            {
                grabbable.GetComponent<DestroyOnCollision>().BlowUp();
            }
        }

        private void OnPowerUpGrabbed(TouchHandGrabInteractor interactor)
        {
            isGrabbed = true;
        }

    }

}
