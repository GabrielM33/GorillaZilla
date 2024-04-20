using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaZilla
{
    [RequireComponent(typeof(PassthroughLayerController))]
    public class Player : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] OVRPassthroughLayer aliveLayer;
        [SerializeField] OVRPassthroughLayer toxicLayer;
        [SerializeField] OVRPassthroughLayer deadLayer;


        [Header("References")]
        [SerializeField] public TimeManipulator timeManipulator;
        [SerializeField] public PlayerMenu menu;
        [SerializeField] List<TriggerEvent> collidableBodyParts;
        [SerializeField] List<PlayerAbility> playerAbilities;
        [SerializeField] AudioSource deathNoise;
        private PassthroughLayerController passthroughLayerController;

        [Header("Events")]
        public UnityEvent onPlayerHit;
        void Start()
        {
            passthroughLayerController = GetComponent<PassthroughLayerController>();
            foreach (var part in collidableBodyParts)
            {
                part.onTriggerEnter.AddListener(OnBodyTriggerCollision);
            }
            foreach (var ability in playerAbilities)
            {
                ability.onAbilityDeactivate.AddListener(OnAbilityDeactive);
            }
        }
        private void OnAbilityDeactive()
        {
            print("Re-activating aliveLayer");
            passthroughLayerController.SetActiveLayer(aliveLayer);
        }
        private void OnBodyTriggerCollision(Collider other)
        {
            Die();
        }

        public void Die()
        {
            passthroughLayerController.SetActiveLayer(deadLayer);
            deathNoise.Play();
            onPlayerHit.Invoke();
        }
        public void Revive()
        {
            passthroughLayerController.SetActiveLayer(aliveLayer);
        }
        public void ActivateAbility(string abilityName)
        {
            passthroughLayerController.SetActiveLayer(toxicLayer);
            foreach (var ability in playerAbilities)
            {
                if (ability.abilityName == abilityName)
                {
                    ability.Activate();
                }
                else
                {
                    ability.Deactivate();
                }
            }
        }

    }
}
