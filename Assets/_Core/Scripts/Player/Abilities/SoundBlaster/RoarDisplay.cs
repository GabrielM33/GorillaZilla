using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoarDisplay : MonoBehaviour
{
    public float voiceMultiplier = 100f;
    public Image volumeBackground;
    public Transform visual;

    public void ToggleVisual(bool isVisible)
    {
        visual.gameObject.SetActive(isVisible);
        if (!isVisible) volumeBackground.fillAmount = 0;
    }
    public void SetRoarVolume(float volume)
    {
        float height = Mathf.Clamp01(volume * voiceMultiplier);
        volumeBackground.fillAmount = height;
    }
}
