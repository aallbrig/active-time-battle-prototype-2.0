using System;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Events;

namespace MonoBehaviours.EventListeners
{
    [Serializable] public class ActionEffectEventUnityEvent : UnityEvent<FighterController, float> {}

    public class ActionEffectEventListener : MonoBehaviour
    {
        public ActionEffectEvent soEffectEvent;
        public ActionEffectEventUnityEvent unityEvent;

        public void OnEnable() => soEffectEvent.RegisterListener(this);

        public void OnDisable() => soEffectEvent.UnregisterListener(this);

        public void OnEventBroadcast(FighterController fighter, float effect) => unityEvent.Invoke(fighter, effect);
    }
}