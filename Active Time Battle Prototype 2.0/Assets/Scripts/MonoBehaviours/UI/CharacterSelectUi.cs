using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.Lists;
using ScriptableObjects.RuntimeSets;
using ScriptableObjects.Vars;
using TMPro;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public class CharacterSelectUi : MonoBehaviour
    {
        public Fighters availableFighters;
        public FighterRuntimeSet selectedFighters;
        public IntVar playerGold;
        public Fighter selectedFighter;
        public GameObjectEvent selectButtonPress;
        public GameObject modelPreview;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI mpText;
        public TextMeshProUGUI actionsText;

        private bool _active;

        private void OnEnable() => RenderUi();
        private void OnDisable()
        {
            selectedFighter = null;
            foreach (Transform child in modelPreview.transform)
                Destroy(child.gameObject);
        }

        public void BroadcastSelectClick()
        {
            _active = true;
            selectButtonPress.Broadcast(gameObject);
        }

        public void OnRandomizeClick()
        {
            var fighter = availableFighters.list[Random.Range(0, availableFighters.list.Count)];

            if (selectedFighter != null)
            {
                playerGold.value += selectedFighter.cost.Value;
                selectedFighters.Remove(selectedFighter);
            }

            if (playerGold.value > fighter.cost.Value)
            {
                selectedFighter = Instantiate(fighter);
                selectedFighters.Add(selectedFighter);
                playerGold.value -= fighter.cost.Value;
            }

            RenderUi();
        }

        public void OnFighterSelect(Fighter fighter)
        {
            if (!_active) return;

            _active = false;

            if (selectedFighter != null)
            {
                playerGold.value += selectedFighter.cost.Value;
                selectedFighters.Remove(selectedFighter);
            }

            if (playerGold.value > fighter.cost.Value)
            {
                selectedFighter = Instantiate(fighter);
                selectedFighters.Add(selectedFighter);
                playerGold.value -= fighter.cost.Value;
            }

            RenderUi();
        }

        private void RenderUi()
        {
            nameText.text = "";
            costText.text = "";
            hpText.text = "";
            mpText.text = "";
            actionsText.text = "";

            if (selectedFighter == null) return;
            var fighterClass = selectedFighter.fighterClass;

            // Clear
            foreach (Transform child in modelPreview.transform)
                Destroy(child.gameObject);

            nameText.text = "Name: " + selectedFighter.fighterName.Value;
            costText.text = "Cost: " + selectedFighter.cost.Value;
            hpText.text = "Health range: " + fighterClass.minHp.Value + " - " + fighterClass.maxHp.Value;
            mpText.text = "Mana range: " + fighterClass.minMp.Value + " - " + fighterClass.maxMp.Value;
            for (var i = 0; i < fighterClass.actions.Count; i++)
            {
                var action = fighterClass.actions[i];
                actionsText.text += action.actionName.Value + " (" + action.effectMin.Value + " - " + action.effectMax.Value +
                                    ")\n";
            }

            Instantiate(selectedFighter.model.Value, modelPreview.transform);
        }
    }
}