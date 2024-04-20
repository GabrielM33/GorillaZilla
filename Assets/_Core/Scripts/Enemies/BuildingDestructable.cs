using System;
using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;

[RequireComponent(typeof(DestroyOnCollision))]
public class BuildingDestructable : MonoBehaviour
{
    public LayerMask buildingLayerMask;
    private DestroyOnCollision destroyOnCollision;
    void Start()
    {
        destroyOnCollision = GetComponent<DestroyOnCollision>();
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, buildingLayerMask, QueryTriggerInteraction.Collide))
        {
            var building = hit.collider.GetComponentInParent<DestructableBuilding>();
            if (building != null)
            {
                building.onBuildingHit.AddListener(OnBuildingHit);
            }
        }
    }

    private void OnBuildingHit()
    {
        destroyOnCollision.BlowUp();
    }
}
