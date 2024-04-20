using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    public class SoundBlaster : PlayerAbility
    {
        [Header("Dependencies")]
        [SerializeField] VoiceDetector roarDetector;


        [Header("References")]
        [SerializeField] RoarDisplay roarDisplay;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject soundBlastPrefab;
        [SerializeField] float timeSpacing = .1f;
        [SerializeField] float voiceRingScalar = 100;
        float timer;
        private void Awake()
        {
            if (roarDetector == null)
            {
                roarDetector = GameObject.FindObjectOfType<VoiceDetector>();
            }
        }

        // Start is called before the first frame update
        public override void Activate()
        {
            base.Activate();
            roarDetector.OnVolumeChanged?.AddListener(OnRoar);
            roarDetector.Listen();
            roarDisplay.ToggleVisual(true);
        }
        public override void Deactivate()
        {
            base.Deactivate();
            roarDetector.OnVolumeChanged?.RemoveListener(OnRoar);
            roarDisplay.ToggleVisual(false);
        }
        protected override void Update()
        {
            base.Update();
            timer -= Time.deltaTime;
        }

        private void OnRoar(float volume)
        {
            if (timer < 0)
            {
                GameObject soundBlast = Instantiate(soundBlastPrefab, firePoint.position, firePoint.rotation);
                soundBlast.GetComponent<SoundBlast>().targetSize = Vector3.one * volume * voiceRingScalar;
                timer = timeSpacing;

                roarDisplay.SetRoarVolume(volume);
            }

        }

    }
}

