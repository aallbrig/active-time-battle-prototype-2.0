using System.Linq;
using MonoBehaviours.StateMachines;
using ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.GameState.Decisions
{
    [CreateAssetMenu(
        fileName = "New detect all fighters dead",
        menuName = "AATools/GameState/Decisions/DetectAllFightersDead",
        order = 0
    )]
    public class DetectAllFightersDead : Decision
    {
        public FighterControllerRuntimeSet fighters;

        public override bool Decide(GameStateContext context) => fighters.list.Count > 0 &&
                                                                 fighters.list.Where(fighter => fighter.currentHp > 0)
                                                                     .ToList().Count == 0;
    }
}