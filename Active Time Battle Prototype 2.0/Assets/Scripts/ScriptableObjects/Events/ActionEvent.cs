using System.Collections.Generic;
using MonoBehaviours.EventListeners;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New action event", menuName = "AATools/Events/ActionEvent")]
    public class ActionEvent : ScriptableObject
    {
        private readonly List<ActionEventListener> _listeners = new List<ActionEventListener>();

        public void Broadcast(Action action)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventBroadcast(action);
        }

        public void RegisterListener(ActionEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(ActionEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}