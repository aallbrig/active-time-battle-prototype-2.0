using System.Collections.Generic;
using MonoBehaviours.Controllers;
using MonoBehaviours.EventListeners;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New fighter controller", menuName = "AATools/Events/FighterController")]
    public class FighterControllerEvent : ScriptableObject
    {
        private readonly List<FighterControllerEventListener> _listeners = new List<FighterControllerEventListener>();

        public void Broadcast(FighterController fighter)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventBroadcast(fighter);
        }

        public void RegisterListener(FighterControllerEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(FighterControllerEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}