﻿using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.RuntimeQueue;
using ScriptableObjects.RuntimeSets;
using UnityEngine;

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

            inputQueue.queue.Clear();
            _selectedAction = null;
            _targets = null;
        }

        public void HandleBattleMeterFull(FighterController fighter)
        {
            if (playerFighters.list.Contains(fighter))
                inputQueue.Enqueue(fighter);
        }

        public void HandleActionSelected(Action action) => _selectedAction = action;
        public void HandleTargetsSelected(List<FighterController> targets) => _targets = targets;

        public void ClearQueue() => inputQueue.queue.Clear();

        public void ResetInput()
        {
            _selectedAction = null;
            _targets = null;
            _activeFighter = null;
        }

        private IEnumerator InputQueueProcessor()
        {
            while (true)
            {
                if (inputQueue.queue.Count > 0)
                {
                    _activeFighter = inputQueue.queue.Dequeue();
                    // Dead player characters can't act
                    if (_activeFighter.currentHp <= 0) continue;

                    activePlayerFighter.Broadcast(_activeFighter);

                    while (_selectedAction == null)
                        if (_activeFighter.currentHp <= 0) break;
                        else yield return null;

                    while (_targets == null)
                        if (_activeFighter.currentHp <= 0) break;
                        else yield return null;

                    // Dead player characters can't act
                    if (_activeFighter.currentHp <= 0) continue;

                    battleCommand.Broadcast(_activeFighter, _selectedAction, _targets);

                    ResetInput();
                }

                yield return null;
            }
        }
    }
}