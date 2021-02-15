using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using UnityEngine;

namespace ScriptableObjects.GameEntities
{
    public class Item : ScriptableObject
    {
        public IEnumerator Use(FighterController controller, List<FighterController> targets)
        {
            yield return null;
        }
    }
}