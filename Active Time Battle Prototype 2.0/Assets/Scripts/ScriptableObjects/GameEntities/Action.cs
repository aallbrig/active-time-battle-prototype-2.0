using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.Refs;
using UnityEngine;

namespace ScriptableObjects.GameEntities
{
    [CreateAssetMenu(fileName = "New action", menuName = "ATB/Action", order = 0)]
    public class Action : ScriptableObject
    {
        public StringRef actionName;
        public ActionPerformance performance;
        public FloatRef stoppingDistance;
        public IntRef effectMin;
        public IntRef effectMax;

        public IntRef mpCost;

        public BoolRef multiple;
        public BoolRef healing;

        public IEnumerator Act(FighterController controller, List<FighterController> targets)
        {
            var effectValue = Random.Range(effectMin.Value, effectMax.Value);

            if (performance != null)
                yield return performance.Perform(controller, targets);

            targets.ForEach(target =>
            {
                if (healing.Value)
                    target.Heal(effectValue);
                else
                    target.Damage(effectValue);
            });
        }


        // (optional) particles effects
        // applyParticleEffectEnum (before, beforeEach, afterEach, after)

        // sound effects
        // applySoundEffectsEnum (before, beforeEach, afterEach, after)
    }
}