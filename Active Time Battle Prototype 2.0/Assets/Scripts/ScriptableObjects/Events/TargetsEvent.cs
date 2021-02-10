using System.Collections.Generic;
using MonoBehaviours.Controllers;
using MonoBehaviours.EventListeners;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New targets event", menuName = "AATools/Events/TargetsEvent")]
    public class TargetsEvent : ScriptableObject
    {
        private readonly List<TargetsEventListener> _listeners = new List<TargetsEventListener>();

        public void Broadcast(List<FighterController> targets)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventBroadcast(targets);
        }

        public void RegisterListener(TargetsEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(TargetsEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}