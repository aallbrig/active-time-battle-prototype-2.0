using System;
using ScriptableObjects.RuntimeSets;
using ScriptableObjects.Vars;
using TMPro;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public class VictoryScreenUi : MonoBehaviour
    {
        public IntVar playerGold;
        public FighterRuntimeSet fightersPlayerHasSlain;
        public TextMeshProUGUI statsText;
        public TextMeshProUGUI goldText;

        private void Update()
        {
            statsText.text = "Body count: " + fightersPlayerHasSlain.list.Count;
            goldText.text = "Gold: " + playerGold.value;
        }
    }
}