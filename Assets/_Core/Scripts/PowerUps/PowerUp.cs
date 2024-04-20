using System;
using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;

namespace GorillaZilla
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] Drinkable drinkable;
        [SerializeField] float duration;
        private float timer;
        private void Awake()
        {
            drinkable.onDrinkFinished.AddListener(OnDrinkFinished);
        }
        private void OnDrinkFinished()
        {
            Activate();
        }
        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    Deactivate();
                }
            }
        }
        public virtual void Activate()
        {
            timer = duration;
            print("Power Up Activated: " + name);
        }
        public virtual void Deactivate()
        {
            print("Power Up Deactivated: " + name);
        }
    }
}

