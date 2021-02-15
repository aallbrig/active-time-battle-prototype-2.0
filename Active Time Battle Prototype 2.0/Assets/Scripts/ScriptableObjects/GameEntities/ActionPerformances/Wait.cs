using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.Refs;
using UnityEngine;

namespace ScriptableObjects.GameEntities.ActionPerformances
{
    [CreateAssetMenu(fileName = "New wait action performance", menuName = "ATB/ActionPerformance/Wait", order = 0)]
    public class Wait : ActionPerformance
    {
        public FloatRef waitInSeconds;

        public override IEnumerator Perform(FighterController controller, List<FighterController> targets)
        {
            yield return new WaitForSeconds(waitInSeconds.Value);
        }
    }
}