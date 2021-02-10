using MonoBehaviours.Controllers;
using UnityEngine;

namespace ScriptableObjects.RuntimeQueue
{
    [CreateAssetMenu(fileName = "New fighter controller runtime queue", menuName = "AATools/RuntimeQueues/FighterController")]
    public class FighterControllerRuntimeQueue : RuntimeQueue<FighterController> {}
}