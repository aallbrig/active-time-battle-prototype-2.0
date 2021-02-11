using System.Collections.Generic;
using ScriptableObjects.GameEntities;
using ScriptableObjects.Refs;
using UnityEngine;

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

        private void Start()
        {
            _fighter = GetComponentInParent<FighterController>();
            _meshRenderer = GetComponent<MeshRenderer>();
            ToDefaultColor();
        }

        private void Update()
        {
            if (_fighter != null && _fighter.currentHp <= 0)
                ToDeadColor();
        }

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
    }
}