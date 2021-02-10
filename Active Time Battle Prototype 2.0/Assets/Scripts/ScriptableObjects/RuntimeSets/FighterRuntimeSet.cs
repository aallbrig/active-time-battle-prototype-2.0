using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.RuntimeSets
{
    [CreateAssetMenu(fileName = "New fighter runtime set", menuName = "AATools/RuntimeSets/Fighter")]
    public class FighterRuntimeSet : RuntimeSet<Fighter> {}
}