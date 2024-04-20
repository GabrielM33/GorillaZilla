using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVisual : MonoBehaviour
{
    public LineRenderer line;
    public float maxDist;
    public LayerMask laserLayer;
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDist, laserLayer))
        {
            line.SetPosition(0, transform.position);
            Vector3 targetPoint = hit.point;
            targetPoint.y = transform.position.y;
            line.SetPosition(1, targetPoint);
        }
    }
}
