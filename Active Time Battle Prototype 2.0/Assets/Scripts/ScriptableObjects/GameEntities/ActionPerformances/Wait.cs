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
        public GameObjectRef particle;
        public bool spawnParticleOnSelf = false;
        public Vector3Ref particleSpawnOffset;
        public FloatRef particleLifetimeInSeconds;
        public FloatRef waitInSeconds;

        public override IEnumerator Perform(FighterController controller, List<FighterController> targets)
        {
            yield return new WaitForSeconds(waitInSeconds.Value);

            if (particle.Value != null)
                if (spawnParticleOnSelf)
                    CreateParticle(controller.transform);
                else
                    targets.ForEach(target => CreateParticle(target.transform));
        }

        private void CreateParticle(Transform transform)
        {
            var particleInstance = Instantiate(particle.Value, transform);
            particleInstance.transform.position += particleSpawnOffset.Value;
            Destroy(particleInstance, particleLifetimeInSeconds.Value);
        }
    }
}