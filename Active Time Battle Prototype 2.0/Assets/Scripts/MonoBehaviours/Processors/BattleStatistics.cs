using ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace MonoBehaviours.Processors
{
    public class BattleStatistics : MonoBehaviour
    {
        public FighterRuntimeSet fightersPlayerHasSlain;
        public FighterControllerRuntimeSet enemyFighters;

        public void AddEnemyFightersToStats()
        {
            enemyFighters.list.ForEach(fighter =>
            {
                fightersPlayerHasSlain.list.Add(Instantiate(fighter.fighterTemplate));
            });
        }
    }
}