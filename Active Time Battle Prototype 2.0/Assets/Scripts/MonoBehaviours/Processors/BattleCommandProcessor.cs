using System;
using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using UnityEngine;
using Action = ScriptableObjects.GameEntities.Action;

namespace MonoBehaviours.Processors
{
    [Serializable]
    public class BattleCommand
    {
        private readonly Action _action;
        private readonly FighterController _fighter;
        private readonly List<FighterController> _targets;

        public BattleCommand(FighterController fighter, Action action, List<FighterController> targets)
        {
            _fighter = fighter;
            _action = action;
            _targets = targets;
        }

        public IEnumerator Execute()
        {
            return _fighter.PerformAction(_action, _targets);
        }
    }

    public class BattleCommandProcessor : MonoBehaviour
    {
        private readonly Queue<BattleCommand> _battleCommandQueue = new Queue<BattleCommand>();

        private void Awake() => StartCoroutine(ProcessBattleCommand());

        public void ClearQueue() => _battleCommandQueue.Clear();
        public void HandleBattleCommandEvent(FighterController fighter, Action action, List<FighterController> targets) =>
            _battleCommandQueue.Enqueue(new BattleCommand(fighter, action, targets));

        private IEnumerator ProcessBattleCommand()
        {
            while (true)
                if (_battleCommandQueue.Count > 0)
                {
                    var battleCommand = _battleCommandQueue.Dequeue();
                    yield return battleCommand.Execute();
                }
                else
                {
                    yield return null;
                }
        }
    }
}