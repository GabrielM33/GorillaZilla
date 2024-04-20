using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    [RequireComponent(typeof(Grid), typeof(BoxCollider))]
    public class RoomManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] OVRSceneManager sceneManager;

        [Header("Settings")]
        [SerializeField] float paddingFromWall = .2f;
        [SerializeField] LayerMask wallLayer;
        [SerializeField] LayerMask furnitureLayer;
        [SerializeField] bool debugSpawnPoints = false;

        private OVRSceneRoom room;
        private BoxCollider roomBounds;
        private Grid grid;
        private TransformFollower follower;

        private void Awake()
        {
            if (sceneManager == null)
                sceneManager = GameObject.FindObjectOfType<OVRSceneManager>();

            sceneManager.SceneModelLoadedSuccessfully += OnSceneLoaded;
            grid = GetComponent<Grid>();
            follower = GetComponent<TransformFollower>();
            roomBounds = GetComponent<BoxCollider>();
            roomBounds.isTrigger = true;

        }
        private void OnSceneLoaded()
        {
            room = GameObject.FindAnyObjectByType<OVRSceneRoom>();
            room.gameObject.SetLayerRecursive("Room");

            //Setup collider for room to fit the bounds of the space (using collider as a Bounds type doesn't work with rotation)
            float height = room.Walls[0].Height;
            float width = room.Floor.Width;
            float depth = room.Floor.Height;
            roomBounds.size = new Vector3(width, depth, height);
            roomBounds.center = new Vector3(0, 0, height / 2f);
            follower.target = room.Floor.transform;
        }
        public bool IsInsideGuardian(Vector3 position)
        {
            position.y = 0;
            var result = OVRManager.boundary.TestPoint(position, OVRBoundary.BoundaryType.PlayArea);
            float dist = result.ClosestDistance;
            return dist <= .1f;
        }

        //Checks if the point is colliding with furniture using a Physics check box
        public bool IsInsideFurniture(Vector3 position)
        {
            bool isInFurniture = Physics.CheckBox(position, grid.cellSize, transform.rotation, furnitureLayer);
            bool isBelowFurniture = Physics.Raycast(position, Vector3.up, Mathf.Infinity, furnitureLayer);
            return isInFurniture || isBelowFurniture;
        }
        //Returns true if a point is facing in the forward direction (i.e. the normal) of the closest wall to that given point
        public bool IsPointInRoom(Vector3 point, float padding = 0)
        {
            float distance = DistanceToClosestWall(point, out Vector3 closestPoint, out OVRScenePlane closestWall);
            if (distance < padding || closestWall == null)
            {
                return false;
            }
            bool isInBoxCollider = roomBounds.ClosestPoint(point) == point;
            Vector3 dirToWall = (closestPoint - point).normalized;
            float dotProduct = Vector3.Dot(closestWall.transform.forward, dirToWall);
            return isInBoxCollider && dotProduct < -.5f;
        }

        //Calculate the closest point to any given wall in the OVRSceneRoom
        public float DistanceToClosestWall(Vector3 point, out Vector3 closestPoint, out OVRScenePlane closestWall)
        {

            float minDist = Mathf.Infinity;
            closestPoint = Vector3.zero;
            closestWall = null;
            if (room == null)
            {
                return minDist;
            }
            foreach (OVRScenePlane wall in room.Walls)
            {
                var col = wall.GetComponentInChildren<Collider>();
                Vector3 cp = col.ClosestPoint(point);
                float dist = Vector3.Distance(cp, point);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestPoint = cp;
                    closestWall = wall;
                }
            }
            return minDist;
        }
        //Returns the center position of the room
        public Vector3 CenterOfRoom()
        {
            Vector3 centerOfRoom = roomBounds.transform.TransformPoint(roomBounds.center);
            return centerOfRoom;
        }
        //Returns a list of positions on a grid that are within the room and not intersecting furniture
        public List<Vector3> GetAvailableSpawnLocations()
        {
            if (grid == null)
            {
                grid = GetComponent<Grid>();
            }
            List<Vector3> result = new List<Vector3>();

            int end = 25;
            int start = -25;

            for (int i = start; i < end; i++)
            {
                for (int j = start; j < end; j++)
                {
                    Vector3Int cell = new Vector3Int(i, 0, j);
                    Vector3 worldPos = grid.CellToWorld(cell);

                    //Add a small offset to account for room movement
                    Vector3 floorOffset = worldPos;
                    floorOffset.y += .5f;

                    bool isInRoom = IsPointInRoom(worldPos, paddingFromWall);
                    bool isInFurniture = IsInsideFurniture(floorOffset);
                    bool isInsideGuardian = IsInsideGuardian(worldPos);

                    bool allowSpawn = isInRoom && !isInFurniture && isInsideGuardian;
                    if (allowSpawn)
                    {
                        result.Add(worldPos);
                    }
                }
            }
            return result;
        }
        private void OnDrawGizmos()
        {
            if (!debugSpawnPoints) return;

            List<Vector3> availableLocations = GetAvailableSpawnLocations();
            Gizmos.color = Color.green;
            foreach (Vector3 location in availableLocations)
            {
                Gizmos.DrawCube(location, grid.cellSize * .9f);
            }
        }

    }
}
