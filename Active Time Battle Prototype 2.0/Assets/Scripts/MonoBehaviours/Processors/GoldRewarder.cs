using ScriptableObjects.RuntimeSets;
using ScriptableObjects.Vars;
using UnityEngine;

namespace MonoBehaviours.Processors
{
    public class GoldRewarder : MonoBehaviour
    {
        public IntVar playerGold;
        public FighterControllerRuntimeSet enemyFighters;

        public void RewardGold()
        {
            enemyFighters.list.ForEach(fighter =>
            {
                var goldReward = Random.Range(fighter.fighterTemplate.goldRewardMin.Value, fighter.fighterTemplate.goldRewardMax.Value);
                playerGold.value += goldReward;
            });
        }
    }
}