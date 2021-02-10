using System.Collections.Generic;
using MonoBehaviours.Controllers;
using MonoBehaviours.EventListeners;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New battle command event", menuName = "AATools/Events/BattleCommandEvent", order = 0)]
    public class BattleCommandEvent : ScriptableObject
    {
        private readonly List<BattleCommandEventListener> _listeners = new List<BattleCommandEventListener>();

        public void Broadcast(FighterController fighter, Action action, List<FighterController> targets)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventBroadcast(fighter, action, targets);
        }

        public void RegisterListener(BattleCommandEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(BattleCommandEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}