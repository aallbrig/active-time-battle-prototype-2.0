using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.Vars;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI
{
    public class FighterCard : MonoBehaviour
    {
        public Fighter Fighter
        {
            get => _fighter;
            set
            {
                _fighter = value;
                RenderUi();
            }
        }

        public FighterEvent fighterSelectedEvent;
        public IntVar playerGold;
        private Fighter _fighter;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI manaText;
        public TextMeshProUGUI actionsText;
        public Button selectButton;
        
        private void RenderUi()
        {
            nameText.text = Fighter.fighterName.Value;
            costText.text = "Cost: " + Fighter.cost.Value;
            healthText.text = "HP: " + Fighter.fighterClass.minHp.Value + " - " + Fighter.fighterClass.maxHp.Value;
            manaText.text = "MP: " + Fighter.fighterClass.minMp.Value + " - " + Fighter.fighterClass.maxMp.Value;
            actionsText.text = "";
            Fighter.fighterClass.actions.ForEach(action =>
                actionsText.text +=
                    action.actionName.Value + " (" + action.effectMin.Value + " - " + action.effectMax.Value + ")\n"
            );
            selectButton.onClick.AddListener(() =>
            {
                playerGold.value -= _fighter.cost.Value;
                fighterSelectedEvent.Broadcast(_fighter);
            });
        }

        private void Update()
        {
            if (Fighter != null)
                selectButton.interactable = playerGold.value > Fighter.cost.Value;
        }
    }
}