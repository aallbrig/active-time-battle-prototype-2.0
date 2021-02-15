using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.RuntimeSets
{
    [CreateAssetMenu(fileName = "New action runtime set", menuName = "AATools/RuntimeSets/Action", order = 0)]
    public class ActionRuntimeSet : RuntimeSet<Action> {}
}