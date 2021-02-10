using System;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using UnityEngine;
using UnityEngine.Events;
using Action = ScriptableObjects.GameEntities.Action;

namespace MonoBehaviours.EventListeners
{
    [Serializable] public class ActionEventUnityEvent : UnityEvent<Action> {}

    public class ActionEventListener : MonoBehaviour
    {
        public ActionEvent soEvent;
        public ActionEventUnityEvent unityEvent;

        public void OnEnable() => soEvent.RegisterListener(this);

        public void OnDisable() => soEvent.UnregisterListener(this);

        public void OnEventBroadcast(Action fighter) => unityEvent.Invoke(fighter);
    }
}