using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.RuntimeQueue
{
    public abstract class RuntimeQueue<T> : ScriptableObject
    {
        public readonly Queue<T> queue = new Queue<T>();

        private void Awake() => queue.Clear();
        private void OnEnable() => queue.Clear();
        private void OnDisable() => queue.Clear();
        private void OnDestroy() => queue.Clear();

        public void Enqueue(T controller)
        {
            if (!queue.Contains(controller)) queue.Enqueue(controller);
        }
    }
}