using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.Lists;
using ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace MonoBehaviours
{
    public class EnemyFighterSpawner : MonoBehaviour
    {
        public Fighters availableFighters;
        public FighterControllerRuntimeSet enemyFighters;
        public List<Transform> enemySpawnPoints = new List<Transform>();
        public FighterController fighterControllerPrefab;

        // Triggered on battle enter
        public void SpawnFighters()
        {
            enemySpawnPoints.ForEach(spawnPoint =>
            {
                foreach (Transform child in spawnPoint.transform)
                {
                    Destroy(child.gameObject);
                    enemyFighters.Remove(child.GetComponent<FighterController>());
                }
            });

            var shuffledSpawnPoints = new List<Transform>();
            shuffledSpawnPoints.AddRange(enemySpawnPoints);
            shuffledSpawnPoints.Sort((a, b) => Random.Range(-1, 1));
            var howManyEnemies = Random.Range(1, enemySpawnPoints.Count + 1);
            for (var i = 0; i < howManyEnemies; i++)
            {
                var randomFighter = availableFighters.list[Random.Range(0, availableFighters.list.Count)];
                var fighterController = Instantiate(fighterControllerPrefab, shuffledSpawnPoints[i]);
                fighterController.fighterTemplate = randomFighter;

                enemyFighters.Add(fighterController);
            }
        }
    }
}