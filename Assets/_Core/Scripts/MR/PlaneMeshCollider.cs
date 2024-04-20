using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMeshCollider : MonoBehaviour
{
    //Note: could fail if job for OVRScenePlaneMeshFilter has not completed
    private void Start()
    {
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }
}
