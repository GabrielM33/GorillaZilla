using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;
using Utilities.XR;

public class RoomManagerDebugger : MonoBehaviour
{

    public RoomManager roomManager;
    private void OnDrawGizmos()
    {
        RenderGizmo();
    }
    private void Update()
    {
        RenderGizmo();
    }
    [ContextMenu("Render Gizmo")]
    void RenderGizmo()
    {
        if (roomManager == null || !PlayerSettings.ShowDebug) return;
        Grid grid = roomManager.GetComponent<Grid>();
        List<Vector3> availableLocations = roomManager.GetAvailableSpawnLocations();
        Gizmos.color = Color.green;
        Vector3 cubeSize = grid.cellSize;
        cubeSize.z = .01f;
        foreach (Vector3 location in availableLocations)
        {
            XRGizmos.DrawCube(location, roomManager.transform.rotation, cubeSize, Color.green);
        }
    }


}
