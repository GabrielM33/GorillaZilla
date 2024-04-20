using System;
using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;

public class Donut : MonoBehaviour
{
    [SerializeField] GameObject donutCompleteModel;
    [SerializeField] GameObject donutHalfEatenModel;
    [SerializeField] AudioSource eatSound;
    [SerializeField] ParticleSystem eatParticle;
    [SerializeField] float eatDelay = 1f;
    private float eatTimer;
    private enum DonutState
    {
        Full,
        Half,
        Eaten
    }
    DonutState donutState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Eat();
        }
    }
    private void Update()
    {
        if (eatTimer > 0)
        {
            eatTimer -= Time.deltaTime;
        }
    }
    public void Eat()
    {
        if (eatTimer > 0) return;
        switch (donutState)
        {
            case DonutState.Full:
                donutState = DonutState.Half;
                donutHalfEatenModel.SetActive(true);
                donutCompleteModel.SetActive(false);
                break;
            case DonutState.Half:
                donutState = DonutState.Eaten;
                donutHalfEatenModel.SetActive(false);
                donutCompleteModel.SetActive(false);
                Destroy(gameObject, 1f);
                break;
        }

        eatSound.Play();
        eatParticle.Play();
        if (donutState == DonutState.Eaten)
        {
            Player player = GameObject.FindObjectOfType<Player>();
            player.ActivateAbility("SoundBlaster");
        }
        eatTimer = eatDelay;
    }
}
