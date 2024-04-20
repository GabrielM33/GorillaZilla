using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaZilla
{
    public class Drinkable : MonoBehaviour
    {
        [SerializeField] float minDrinkDistance = .3f;
        [SerializeField] float drinkSpeed = 1f;
        [SerializeField] float fill = .6f;
        [SerializeField] MeshRenderer liquidRenderer;
        [SerializeField] ParticleSystem particles;
        public UnityEvent onDrinkStart;
        public UnityEvent onDrinkStop;
        public UnityEvent onDrinkFinished;
        private Transform head;
        private bool isDrinking = false;


        private void Start()
        {
            head = Camera.main.transform;
            liquidRenderer.material.SetFloat("_Fill", (fill * .1f) + .5f);
        }

        private void Update()
        {
            bool isNearHead = Vector3.Distance(head.position, transform.position) < minDrinkDistance;
            bool isFacingDown = Vector3.Dot(Vector3.down, transform.up) > 0;
            bool isEmpty = fill <= 0;
            bool canDrink = isNearHead && isFacingDown && !isEmpty;

            if (canDrink != isDrinking)
            {
                isDrinking = canDrink;
                if (isDrinking)
                {
                    DrinkStart();
                }
                else
                {
                    DrinkStop();
                }
            }
            if (isDrinking)
            {
                fill -= Time.deltaTime * drinkSpeed;
                liquidRenderer.material.SetFloat("_Fill", (fill * .1f) + .5f);
                if (fill <= 0)
                {
                    DrinkFinished();
                }
            }
        }
        private void DrinkStart()
        {
            onDrinkStart.Invoke();
            particles.Play();
        }
        private void DrinkStop()
        {
            onDrinkStop.Invoke();
            particles.Stop();
        }
        private void DrinkFinished()
        {
            onDrinkFinished.Invoke();
            particles.Stop();
        }
    }
}

