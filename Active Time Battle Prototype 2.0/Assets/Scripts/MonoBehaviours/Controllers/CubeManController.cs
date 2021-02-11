using System;
using System.Collections.Generic;
using ScriptableObjects.Refs;
using UnityEngine;
using Action = ScriptableObjects.GameEntities.Action;

namespace MonoBehaviours.Controllers
{
    public class CubeManController : MonoBehaviour
    {
        public ColorRef defaultColor;
        public ColorRef actionStartColor;
        public ColorRef battleMeterFullColor;
        public ColorRef battleCommandReady;
        public ColorRef deadColor;

        private FighterController _fighter;
        private MeshRenderer _meshRenderer;

        public void HandleBattleCommandReady(FighterController fighter, Action action, List<FighterController> targets)
        {
            if (fighter == _fighter)
                ToBattleCommandReadyColor();
        }

        public void HandleBattleMeterFull(FighterController fighter)
        {
            if (fighter == _fighter)
                ToReadyColor();
        }

        public void HandleFighterActionStart(FighterController fighter)
        {
            if (fighter == _fighter)
                ToActionStartColor();
        }

        public void HandleFighterActionComplete(FighterController fighter)
        {
            if (fighter == _fighter)
                ToDefaultColor();
        }

        private void ToActionStartColor() => _meshRenderer.material.color = actionStartColor.Value;
        private void ToDefaultColor() => _meshRenderer.material.color = defaultColor.Value;
        private void ToReadyColor() => _meshRenderer.material.color = battleMeterFullColor.Value;
        private void ToBattleCommandReadyColor() => _meshRenderer.material.color = battleCommandReady.Value;
        private void ToDeadColor() => _meshRenderer.material.color = deadColor.Value;

        private void Update()
        {
            if (_fighter.currentHp <= 0)
                ToDeadColor();
        }

        private void Start()
        {
            _fighter = GetComponentInParent<FighterController>();
            _meshRenderer = GetComponent<MeshRenderer>();
            ToDefaultColor();
        }
    }
}