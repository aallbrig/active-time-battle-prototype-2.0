using System;
using System.Collections;
using MonoBehaviours.Controllers;
using ScriptableObjects.Events;
using ScriptableObjects.Refs;
using ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace MonoBehaviours.Processors
{
    public class BattleMeterTicker : MonoBehaviour
    {
        public FighterControllerRuntimeSet playerFighters;
        public FighterControllerRuntimeSet enemyFighters;
        public FighterControllerEvent battleMeterTick;
        public FighterControllerEvent fighterBattleMeterFull;
        public BoolRef shouldTick;
        public FloatRef battleMeterTickWait;

        private IEnumerator _battleTickerCoroutine;

        private void BattleMeterTick(FighterController fighter)
        {
            if (fighter.currentBattleMeter >= 1.0f) return;

            fighter.BattleMeterTick(battleMeterTickWait.Value);
            battleMeterTick.Broadcast(fighter);

            if (fighter.currentBattleMeter >= 1.0f)
                fighterBattleMeterFull.Broadcast(fighter);
        }

        private IEnumerator BattleTickerCoroutine()
        {
            while (true)
            {
                if (shouldTick.Value)
                {
                    playerFighters.list.ForEach(BattleMeterTick);
                    enemyFighters.list.ForEach(BattleMeterTick);
                }

                yield return new WaitForSeconds(battleMeterTickWait.Value);
            }
        }

        private void Awake()
        {
            _battleTickerCoroutine = BattleTickerCoroutine();
            StartCoroutine(_battleTickerCoroutine);
        }

        private void OnDisable()
        {
            StopCoroutine(_battleTickerCoroutine);
        }
    }
}