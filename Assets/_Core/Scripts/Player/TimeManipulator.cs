using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
///     This script manipulates the time scale based on velocity of player's hands and head
/// </summary>
public class TimeManipulator : MonoBehaviour
{
    [SerializeField] XRNode head;
    [SerializeField] Rigidbody leftHand;
    [SerializeField] Rigidbody rightHand;
    [SerializeField] float sensitivity = 0.8f;
    [SerializeField] float handSensitivity = .1f;
    [SerializeField] float minTimeScale = 0.05f;

    [SerializeField] float timeStep;
    void Update()
    {
        float velocityMagnitude = CalculateVelocity(head).magnitude + ((leftHand.velocity.magnitude + rightHand.velocity.magnitude) * handSensitivity);
        timeStep = Mathf.Max(minTimeScale, velocityMagnitude * sensitivity);
        Time.timeScale = timeStep;
        Time.fixedDeltaTime = timeStep * 0.02f;
    }
    Vector3 CalculateVelocity(XRNode t)
    {
        List<XRNodeState> nodes = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodes);
        foreach (XRNodeState node in nodes)
        {
            if (node.nodeType == t)
            {
                node.TryGetVelocity(out Vector3 vel);
                return vel;
            }
        }
        return Vector3.zero;
    }

    //Reset time when disabled
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }
}
