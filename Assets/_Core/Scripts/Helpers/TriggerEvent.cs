using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
///     Turns OnTrigger callbacks into Unity Events
/// </summary>
public class TriggerEvent : MonoBehaviour
{
    [System.Serializable]
    public class ColliderEvent : UnityEvent<Collider> { }
    public ColliderEvent onTriggerEnter;
    public ColliderEvent onTriggerExit;
    public string targetTag;
    private void OnTriggerEnter(Collider other)
    {
        if (targetTag != "" && other.CompareTag(targetTag))
            onTriggerEnter?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag != "" && other.CompareTag(targetTag))
            onTriggerExit?.Invoke(other);
    }

}
