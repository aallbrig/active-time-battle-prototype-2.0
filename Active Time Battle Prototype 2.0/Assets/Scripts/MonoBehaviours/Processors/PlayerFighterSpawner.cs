using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace MonoBehaviours.Processors
{
    public class PlayerFighterSpawner : MonoBehaviour
    {
        public FighterRuntimeSet selectedFighters;
        public FighterControllerRuntimeSet playerFighters;
        public List<Transform> playerSpawnPoints = new List<Transform>();
        public FighterController fighterControllerPrefab;

        // Triggered on character select state leave
        public void SpawnFighters()
        {
            // Clear spawn points
            playerSpawnPoints.ForEach(spawnPoint =>
            {
                foreach (Transform child in spawnPoint.transform)
                {
                    Destroy(child.gameObject);
                    playerFighters.Remove(child.GetComponent<FighterController>());
                }
            });

            // Spawn players
            if (selectedFighters.list.Count > 0)
            {
                for (var i = 0; i < selectedFighters.list.Count; i++)
                {
                    var fighter = selectedFighters.list[i];
                    var spawnPoint = playerSpawnPoints[i];

                    if (spawnPoint != null)
                    {
                        var fighterController = Instantiate(fighterControllerPrefab, spawnPoint);
                        fighterController.fighterTemplate = fighter;
                        playerFighters.Add(fighterController);
                    }
                }
            }

            // Clear selection
            selectedFighters.list.Clear();
        }
    }
}