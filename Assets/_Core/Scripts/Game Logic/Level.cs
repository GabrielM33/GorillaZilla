using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaZilla
{
    public class Level : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] RoomManager roomManager;

        [Header("Events")]
        public UnityEvent onWaveSpawned;
        public UnityEvent onLastEnemyDestroyed;

        private Transform buildingsRoot;
        private Transform enemiesRoot;
        private float buildingSpawnDelay = .025f;
        private List<Enemy> spawnedEnemies = new List<Enemy>();

        private void Awake()
        {
            if (roomManager == null)
            {
                roomManager = GameObject.FindObjectOfType<RoomManager>();
            }
            buildingsRoot = new GameObject("Buildings").transform;
            buildingsRoot.parent = transform;
            enemiesRoot = new GameObject("Enemies").transform;
            enemiesRoot.parent = transform;
        }
        private void OnEnemyDestroyed(Enemy enemy)
        {
            spawnedEnemies.Remove(enemy);

            print("Enemy destroyed, remaining: " + spawnedEnemies.Count);
            if (spawnedEnemies.Count <= 0)
            {
                onLastEnemyDestroyed.Invoke();
            }
        }

        public IEnumerator SpawnWave(Wave wave)
        {
            spawnedEnemies.Clear();
            int numBuildings = wave.numBuildings;
            int numEnemies = wave.numEnemies;
            List<Transform> enemySpawnPoints = new List<Transform>();
            List<Vector3> availableLocations = roomManager.GetAvailableSpawnLocations();
            List<DestructableBuilding> destructableBuildings = new List<DestructableBuilding>();

            //Spawn Buildings
            for (int i = 0; i < numBuildings + 1; i++)
            {

                if (i > availableLocations.Count - 1)
                {
                    break;
                }
                //Get random position from grid
                int randomIndex = UnityEngine.Random.Range(0, availableLocations.Count);
                Vector3 spawnPosition = availableLocations[randomIndex];
                availableLocations.RemoveAt(randomIndex);

                //Get Random 90 degree rotation;
                float randomYRot = UnityEngine.Random.Range(0, 4) * 90f;
                Quaternion spawnRotation = Quaternion.Euler(0, randomYRot, 0);
                Spawnable spawnable;
                if (i == 0)
                {
                    //Spawn power-up building
                    spawnable = wave.powerUpBuilding;
                }
                else
                {
                    //Get weighted random building
                    spawnable = wave.GetRandomBuilding();
                }
                GameObject go = Instantiate(spawnable.prefab, spawnPosition, spawnRotation, buildingsRoot);

                //Add possible enemy spawn points
                SpawnPointList buildingSpawnPoints = go.GetComponent<SpawnPointList>();
                if (buildingSpawnPoints != null)
                {
                    enemySpawnPoints.AddRange(buildingSpawnPoints.spawnPoints);
                }

                DestructableBuilding destructableBuilding = go.GetComponent<DestructableBuilding>();
                if (destructableBuilding != null)
                {
                    //Make buildings indestructable during setup process
                    destructableBuilding.isDestructable = false;
                    destructableBuildings.Add(destructableBuilding);
                }
                yield return new WaitForSeconds(buildingSpawnDelay);
            }
            yield return new WaitForSeconds(1);

            //Spawn Enemies
            for (int i = 0; i < numEnemies; i++)
            {
                //Get random spawn point
                if (enemySpawnPoints.Count == 0) break;

                int randomIndex = UnityEngine.Random.Range(0, enemySpawnPoints.Count);
                Transform spawnPoint = enemySpawnPoints[randomIndex];
                enemySpawnPoints.Remove(spawnPoint);

                //Get weighted random enemy
                Spawnable spawnable = wave.GetRandomEnemy();
                GameObject go = Instantiate(spawnable.prefab, spawnPoint.position, spawnPoint.rotation, enemiesRoot);

                //Set up callbacks
                Enemy enemy = go.GetComponent<Enemy>();
                if (enemy != null)
                {
                    spawnedEnemies.Add(enemy);
                    enemy.onDestroy?.AddListener(OnEnemyDestroyed);

                }

            }
            foreach (var building in destructableBuildings)
            {
                building.isDestructable = true;
            }

            onWaveSpawned?.Invoke();
        }
        public void ClearLevel()
        {
            spawnedEnemies.Clear();
            buildingsRoot.gameObject.DestroyChildren();
            enemiesRoot.gameObject.DestroyChildren();
        }
        public IEnumerator ClearLevelAnimated(float duration)
        {
            spawnedEnemies.Clear();
            enemiesRoot.gameObject.DestroyChildren();
            var shrinkAnimators = GetComponentsInChildren<GrowShrinkAnimation>();
            foreach (var anim in shrinkAnimators)
            {
                anim.ShrinkAndDestroy(duration);
            }
            yield return new WaitForSeconds(duration);
            buildingsRoot.gameObject.DestroyChildren();
        }
    }
}
