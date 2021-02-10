using MonoBehaviours.StateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.GameState.Decisions
{
    [CreateAssetMenu(fileName = "New Next", menuName = "AATools/GameState/Decisions/Next", order = 0)]
    public class Next : Decision
    {
        public override bool Decide(GameStateContext context) => true;
    }
}