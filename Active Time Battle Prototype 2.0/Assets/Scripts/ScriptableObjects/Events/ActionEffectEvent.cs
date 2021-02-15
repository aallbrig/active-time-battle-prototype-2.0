using System.Collections.Generic;
using MonoBehaviours.Controllers;
using MonoBehaviours.EventListeners;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New action effect event", menuName = "AATools/Events/ActionEffectEvent")]
    public class ActionEffectEvent : ScriptableObject
    {
        private readonly List<ActionEffectEventListener> _listeners = new List<ActionEffectEventListener>();

        public void Broadcast(FighterController fighter, float effect)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventBroadcast(fighter, effect);
        }

        public void RegisterListener(ActionEffectEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(ActionEffectEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}