using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    public string abilityName;
    public float abilityLength = 5f;
    private float abilityTimer;

    [Header("Ability Events")]
    public UnityEvent onAbilityActive;
    public UnityEvent onAbilityDeactivate;
    public virtual void Activate()
    {
        abilityTimer = abilityLength;
        onAbilityActive?.Invoke();
    }
    public virtual void Deactivate()
    {
        print("Deactivateing ability");
        onAbilityDeactivate?.Invoke();
    }
    protected virtual void Update()
    {
        if (abilityTimer > 0)
        {
            abilityTimer -= Time.deltaTime;
            if (abilityTimer <= 0)
            {
                Deactivate();
            }
        }

    }
}
