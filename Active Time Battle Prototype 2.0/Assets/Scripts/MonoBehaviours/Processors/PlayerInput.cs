﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using ScriptableObjects.RuntimeQueue;
using ScriptableObjects.RuntimeSets;
using UnityEngine;
using Action = ScriptableObjects.GameEntities.Action;

namespace MonoBehaviours.Processors
{
    public class PlayerInput : MonoBehaviour
    {
        public BattleCommandEvent battleCommand;
        public FighterControllerRuntimeSet playerFighters;
        public FighterControllerRuntimeQueue inputQueue;
        public FighterControllerEvent activePlayerFighter;
        private FighterController _activeFighter;

        private IEnumerator _inputQueueProcessor;
        private Action _selectedAction;
        private List<FighterController> _targets;

        private void OnEnable()
        {
            _inputQueueProcessor = InputQueueProcessor();
            StartCoroutine(_inputQueueProcessor);
        }
        private void OnDisable()
        {
            if (_inputQueueProcessor != null)
                StopCoroutine(_inputQueueProcessor);
        }

        public void HandleBattleMeterFull(FighterController fighter)
        {
            if (playerFighters.list.Contains(fighter) && !inputQueue.queue.Contains(fighter))
                inputQueue.Enqueue(fighter);
        }

        public void HandleActionSelected(Action action) => _selectedAction = action;
        public void HandleTargetsSelected(List<FighterController> targets) => _targets = targets;

        public void ClearQueue() => inputQueue.queue.Clear();

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

                    while (_targets.Where(fighter => fighter.currentHp > 0).Where(fighter => !fighter.ready).ToList().Count >
                           0)
                        yield return null;

                    battleCommand.Broadcast(_activeFighter, _selectedAction, _targets);
                    yield return null;

                    _selectedAction = null;
                    _targets = null;
                    _activeFighter = null;
                }

                yield return null;
            }
        }
    }
}