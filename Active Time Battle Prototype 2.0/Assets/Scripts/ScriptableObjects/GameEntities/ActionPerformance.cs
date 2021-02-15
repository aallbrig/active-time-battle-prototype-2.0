using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using UnityEngine;

namespace ScriptableObjects.GameEntities
{
    public abstract class ActionPerformance : ScriptableObject
    {
        public abstract IEnumerator Perform(FighterController controller, List<FighterController> targets);
    }
}