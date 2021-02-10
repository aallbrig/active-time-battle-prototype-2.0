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

        // // ingress IEnumerator
        // public IEnumerator Ingress(FighterController controller)
        // {
        //     yield return null;
        // }

        // // actionSequence IEnumerator
        // public IEnumerator Act(FighterController controller)
        // {
        //     yield return null;
        // }

        // // egress IEnumerator
        // public IEnumerator Egress(FighterController controller)
        // {
        //     yield return null;
        // }


        // (optional) particles effects
        // applyParticleEffectEnum (before, beforeEach, afterEach, after)

        // sound effects
        // applySoundEffectsEnum (before, beforeEach, afterEach, after)
    }
}