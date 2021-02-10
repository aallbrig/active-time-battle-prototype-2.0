using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.Refs;
using ScriptableObjects.RuntimeQueue;
using ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace MonoBehaviours.Processors
{
    public class PlayerInput : MonoBehaviour
    {
        public FighterControllerRuntimeSet playerFighters;
        public FighterControllerRuntimeSet enemyFighters;
        public FighterControllerRuntimeQueue inputQueue;
        public FighterControllerEvent activePlayerFighter;

        private IEnumerator _inputQueueProcessor;
        private FighterController _activeFighter;
        private Action _selectedAction;
        private List<FighterController> _targets;

        public void HandleBattleMeterFull(FighterController fighter)
        {
            if (playerFighters.list.Contains(fighter) && !inputQueue.queue.Contains(fighter))
                inputQueue.Enqueue(fighter);
        }

        public void HandleActionSelected(Action action) => _selectedAction = action;
        public void HandleTargetsSelected(List<FighterController> targets) => _targets = targets;

        public void ClearQueue() => inputQueue.queue.Clear();

        private List<FighterController> GetAppropriateTargetsForAction(Action action)
        {
            var list = action.healing.Value ? playerFighters.list : enemyFighters.list;
            var targets = action.multiple.Value ? list : new List<FighterController> {list[Random.Range(0, list.Count)]};
            return targets;
        }

        private IEnumerator InputQueueProcessor()
        {
            while (true)
            {
                if (inputQueue.queue.Count > 0)
                {
                    _activeFighter = inputQueue.queue.Dequeue();
                    activePlayerFighter.Broadcast(_activeFighter);

                    while (_selectedAction == null)
                        yield return null;

                    while (_targets == null)
                        yield return null;

                    while (_targets.Where(fighter => !fighter.ready).ToList().Count > 0)
                        yield return null;

                    yield return _activeFighter.PerformAction(_selectedAction, _targets);
                    _activeFighter.ResetBattleMeter();
                }

                yield return null;
            }
        }

        private void Start()
        {
            _inputQueueProcessor = InputQueueProcessor();
            StartCoroutine(_inputQueueProcessor);
        }
    }
}