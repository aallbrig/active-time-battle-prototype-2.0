using MonoBehaviours.Controllers;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.RuntimeSets
{
    [CreateAssetMenu(fileName = "New fighter controller runtime set", menuName = "AATools/RuntimeSets/FighterController")]
    public class FighterControllerRuntimeSet : RuntimeSet<FighterController> {}
}