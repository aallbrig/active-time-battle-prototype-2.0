using System;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using UnityEngine;
using UnityEngine.Events;

namespace MonoBehaviours.EventListeners
{
    [Serializable] public class FighterEventUnityEvent : UnityEvent<Fighter> {}

    public class FighterEventListener : MonoBehaviour
    {
        public FighterEvent soEvent;
        public FighterEventUnityEvent unityEvent;

        public void OnEnable() => soEvent.RegisterListener(this);

        public void OnDisable() => soEvent.UnregisterListener(this);

        public void OnEventBroadcast(Fighter fighter) => unityEvent.Invoke(fighter);
    }
}