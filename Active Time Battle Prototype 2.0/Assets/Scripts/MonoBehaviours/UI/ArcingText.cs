using System.Collections;
using UnityEngine;

namespace MonoBehaviours.UI
{
    [RequireComponent(typeof(Rigidbody))]
    public class ArcingText : MonoBehaviour
    {
        public float force = 0.8f;
        public float time = 1.0f;

        private void OnEnable()
        {
            // Jitter
            var direction = Vector3.up;
            direction.x = Random.Range(-0.9f, 0.9f);

            if (!(Camera.main is null))
                transform.rotation = Camera.main.transform.rotation;

            GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}