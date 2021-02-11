using UnityEngine;

namespace MonoBehaviours.Utils
{
    public class ConstantRotation : MonoBehaviour
    {
        public float rotationSpeed;
        private Transform _transform;

        private void Start() => _transform = transform;
        private void Update() => _transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}