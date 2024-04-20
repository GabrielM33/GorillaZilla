using Meta.WitAi.Data;
using Oculus.Voice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class VolumeChangedEvent : UnityEvent<float> { }
/// <summary>
///     Handles events for voice input
/// </summary>
public class VoiceDetector : MonoBehaviour
{
    [SerializeField] AppVoiceExperience voice;
    public UnityEvent onListenStart;
    public UnityEvent OnListenEnd;
    public VolumeChangedEvent OnVolumeChanged = new VolumeChangedEvent();

    void Start()
    {
        voice.VoiceEvents.OnStartListening.AddListener(OnListen);
        voice.VoiceEvents.OnStoppedListening.AddListener(OnStopListening);
        voice.AudioEvents.OnMicAudioLevelChanged.AddListener(OnMicLevelChanged);
    }

    private void OnMicLevelChanged(float volume)
    {
        OnVolumeChanged.Invoke(volume);
    }
    private void OnListen()
    {
        print("Listening");
    }
    private void OnStopListening()
    {
        OnListenEnd.Invoke();
    }

    [ContextMenu("Listen")]
    public void Listen()
    {
        voice.ActivateImmediately();
        onListenStart.Invoke();
    }
}
