using System;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Events;

namespace MonoBehaviours.EventListeners
{
    [Serializable] public class FighterControllerEventUnityEvent : UnityEvent<FighterController> {}

    public class FighterControllerEventListener : MonoBehaviour
    {
        public FighterControllerEvent soEvent;
        public FighterControllerEventUnityEvent unityEvent;

        public void OnEnable() => soEvent.RegisterListener(this);

        public void OnDisable() => soEvent.UnregisterListener(this);

        public void OnEventBroadcast(FighterController fighter) => unityEvent.Invoke(fighter);
    }
}