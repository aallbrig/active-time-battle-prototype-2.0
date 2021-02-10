using System;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Events;
using Action = ScriptableObjects.GameEntities.Action;

namespace MonoBehaviours.EventListeners
{
    [Serializable]
    public class BattleCommandEventUnityEvent : UnityEvent<FighterController, Action, List<FighterController>> {}

    public class BattleCommandEventListener : MonoBehaviour
    {
        public BattleCommandEvent soEvent;
        public BattleCommandEventUnityEvent unityEvent;

        public void OnEnable() => soEvent.RegisterListener(this);

        public void OnDisable() => soEvent.UnregisterListener(this);

        public void OnEventBroadcast(FighterController fighter, Action action, List<FighterController> targets) =>
            unityEvent.Invoke(fighter, action, targets);
    }
}