using ScriptableObjects.GameEntities;
using TMPro;
using UnityEngine;

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
        private Fighter _fighter;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI manaText;
        public TextMeshProUGUI actionsText;
        
        private void RenderUi()
        {
            nameText.text = Fighter.fighterName.Value;
            healthText.text = "HP: " + Fighter.fighterClass.minHp.Value + " - " + Fighter.fighterClass.maxHp.Value;
            manaText.text = "MP: " + Fighter.fighterClass.minMp.Value + " - " + Fighter.fighterClass.maxMp.Value;
            actionsText.text = "";
            Fighter.fighterClass.actions.ForEach(action =>
                actionsText.text +=
                    action.actionName.Value + " (" + action.effectMin.Value + " - " + action.effectMax.Value + ")\n"
            );
        }
    }
}