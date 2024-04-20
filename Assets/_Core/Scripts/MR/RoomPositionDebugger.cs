using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    public class RoomPositionDebugger : MonoBehaviour
    {
        [SerializeField] RoomManager roomManager;

        private void OnDrawGizmos()
        {
            if (roomManager == null) return;


            bool isInRoom = roomManager.IsPointInRoom(transform.position);
            bool isInFurniture = roomManager.IsInsideFurniture(transform.position);
            bool isAvailable = isInRoom && !isInFurniture;
            print("Closest Wall: " + roomManager.DistanceToClosestWall(transform.position, out Vector3 closestPoint, out OVRScenePlane closestWall));

            Vector3 centerOfRoom = roomManager.CenterOfRoom();
            Vector3 dir = (centerOfRoom - transform.position).normalized;
            Gizmos.color = isAvailable ? Color.green : Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one * .3f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(closestPoint, .3f);
            // Gizmos.DrawRay(transform.position, dir);
        }
    }

}
