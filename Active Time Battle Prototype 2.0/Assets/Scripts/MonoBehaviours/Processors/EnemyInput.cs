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
    public class EnemyInput : MonoBehaviour
    {
        public BattleCommandEvent battleCommand;
        public FighterControllerRuntimeSet playerFighters;
        public FighterControllerRuntimeSet enemyFighters;
        public FloatRef aiWaitMin;
        public FloatRef aiWaitMax;
        public FighterControllerRuntimeQueue inputQueue;

        private IEnumerator _inputQueueProcessor;

        public void HandleBattleMeterFull(FighterController fighter)
        {
            if (enemyFighters.list.Contains(fighter) && !inputQueue.queue.Contains(fighter))
                inputQueue.Enqueue(fighter);
        }

        public void ClearQueue() => inputQueue.queue.Clear();

        private List<FighterController> GetAppropriateTargetsForAction(Action action)
        {
            var list = action.healing.Value ? enemyFighters.list : playerFighters.list;
            var targets = action.multiple.Value ? list : new List<FighterController>() {list[Random.Range(0, list.Count)]};
            return targets.Where(fighter => fighter.currentHp > 0).ToList();
        }

        private IEnumerator InputQueueProcessor()
        {
            while (true)
            {
                if (inputQueue.queue.Count > 0)
                {
                    var activeFighter = inputQueue.queue.Dequeue();
                    yield return new WaitForSeconds(Random.Range(aiWaitMin.Value, aiWaitMax.Value));

                    Action selectedAction = null;
                    while (selectedAction == null)
                    {
                        var randomAction = activeFighter.Actions[Random.Range(0, activeFighter.Actions.Count)];
                        if (activeFighter.currentMp > randomAction.mpCost.Value)
                            selectedAction = randomAction;
                        yield return null;
                    }
                    yield return new WaitForSeconds(Random.Range(aiWaitMin.Value, aiWaitMax.Value));

                    var targets = GetAppropriateTargetsForAction(selectedAction);
                    yield return new WaitForSeconds(Random.Range(aiWaitMin.Value, aiWaitMax.Value));

                    while (targets.Where(fighter => !fighter.ready).ToList().Count > 0)
                    {
                        yield return null;
                    }

                    battleCommand.Broadcast(activeFighter, selectedAction, targets);
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