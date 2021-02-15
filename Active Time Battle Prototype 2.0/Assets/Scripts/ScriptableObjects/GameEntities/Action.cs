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
        public FloatRef stoppingDistance;
        public IntRef effectMin;

        public IntRef effectMax;
        // applyEffectEnum (before, beforeEach, afterEach, after)

        public IntRef mpCost;

        public BoolRef multiple;
        public BoolRef healing;

        public IEnumerator Act(FighterController controller, List<FighterController> targets)
        {
            var effectValue = Random.Range(effectMin.Value, effectMax.Value);
            targets.ForEach(target =>
            {
                if (healing.Value)
                    target.Heal(effectValue);
                else
                    target.Damage(effectValue);
            });

            yield return new WaitForSeconds(0.25f);
        }


        // (optional) particles effects
        // applyParticleEffectEnum (before, beforeEach, afterEach, after)

        // sound effects
        // applySoundEffectsEnum (before, beforeEach, afterEach, after)
    }
}