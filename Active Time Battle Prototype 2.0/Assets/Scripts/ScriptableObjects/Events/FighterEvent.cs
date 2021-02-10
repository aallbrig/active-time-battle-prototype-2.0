using System.Collections.Generic;
using MonoBehaviours.EventListeners;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New fighter event", menuName = "AATools/Events/FighterEvent")]
    public class FighterEvent : ScriptableObject
    {
        private readonly List<FighterEventListener> _listeners = new List<FighterEventListener>();

        public void Broadcast(Fighter fighter)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventBroadcast(fighter);
        }

        public void RegisterListener(FighterEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(FighterEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}