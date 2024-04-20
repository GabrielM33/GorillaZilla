using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Applies mic input to an audio source to be used for voice modulation
/// </summary>
public class MicToAudioSource : MonoBehaviour
{
    //Note: Disables other scripts from listening to the mic.
    void Start()
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audio.Play();
    }
}
