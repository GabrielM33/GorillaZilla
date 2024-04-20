using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 1)]
    public class Wave : ScriptableObject
    {
        public int numBuildings = 0;
        public int numEnemies = 0;
        public List<Spawnable> buildings;
        public List<Spawnable> enemies;
        public Spawnable powerUpBuilding;
        public Spawnable GetRandomBuilding()
        {
            return GetRandomSpawnable(buildings);
        }
        public Spawnable GetRandomEnemy()
        {
            return GetRandomSpawnable(enemies);
        }
        private Spawnable GetRandomSpawnable(List<Spawnable> spawnables)
        {
            float[] weights = new float[spawnables.Count];
            for (int i = 0; i < spawnables.Count; i++)
            {
                weights[i] = spawnables[i].weightedSpawnProbabilty;
            }
            int index = GetRandomWeightedIndex(weights);
            return spawnables[index];
        }
        private int GetRandomWeightedIndex(float[] weights)
        {
            // Get the total sum of all the weights.
            float weightSum = 0f;
            for (int i = 0; i < weights.Length; ++i)
            {
                weightSum += weights[i];
            }

            // Step through all the possibilities, one by one, checking to see if each one is selected.
            int index = 0;
            int lastIndex = weights.Length - 1;
            while (index < lastIndex)
            {
                // Do a probability check with a likelihood of weights[index] / weightSum.
                if (Random.Range(0, weightSum) < weights[index])
                {
                    return index;
                }

                // Remove the last item from the sum of total untested weights and try again.
                weightSum -= weights[index++];
            }

            // No other item was selected, so return very last index.
            return index;
        }

        public static Wave Copy(Wave waveTemplate)
        {
            Wave wave = ScriptableObject.CreateInstance<Wave>();
            wave.numBuildings = waveTemplate.numBuildings;
            wave.numEnemies = waveTemplate.numEnemies;
            wave.buildings = waveTemplate.buildings;
            wave.powerUpBuilding = waveTemplate.powerUpBuilding;
            wave.enemies = waveTemplate.enemies;
            return wave;
        }
    }
    [System.Serializable]
    public class Spawnable
    {
        public GameObject prefab;
        public int weightedSpawnProbabilty;
    }


}
