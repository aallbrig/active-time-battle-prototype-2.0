using System;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Events;

namespace MonoBehaviours.EventListeners
{
    [Serializable] public class TargetsEventUnityEvent : UnityEvent<List<FighterController>> {}

    public class TargetsEventListener : MonoBehaviour
    {
        public TargetsEvent soEvent;
        public TargetsEventUnityEvent unityEvent;

        public void OnEnable() => soEvent.RegisterListener(this);

        public void OnDisable() => soEvent.UnregisterListener(this);

        public void OnEventBroadcast(List<FighterController> fighter) => unityEvent.Invoke(fighter);
    }
}