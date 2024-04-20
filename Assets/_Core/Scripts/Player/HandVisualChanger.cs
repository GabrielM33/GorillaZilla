using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class HandModelChanger : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] SyntheticHand leftSyntheticHand;
    [SerializeField] SyntheticHand rightSyntheticHand;

    [Header("References")]
    [SerializeField] HandVisual leftHandVisual;
    [SerializeField] HandVisual rightHandVisual;

    private void Awake()
    {
        SetupHands();
    }
    void SetupHands()
    {
        SetupHand(leftSyntheticHand, leftHandVisual);
        SetupHand(rightSyntheticHand, rightHandVisual);
    }
    void SetupHand(SyntheticHand hand, HandVisual targetVisual)
    {
        HandVisual currentVisual = hand.GetComponentInChildren<HandVisual>(true);
        if (currentVisual)
            currentVisual.gameObject.SetActive(false);
        targetVisual.InjectHand(hand);
    }
}
