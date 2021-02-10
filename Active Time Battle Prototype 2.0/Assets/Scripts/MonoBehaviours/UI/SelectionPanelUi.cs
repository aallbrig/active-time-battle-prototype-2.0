using System.Collections.Generic;
using MonoBehaviours.Customizers;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI
{
    public class SelectionPanelUi : MonoBehaviour
    {
        public List<Fighter> selectableFighters = new List<Fighter>();
        public Transform viewportContent;
        public GameObject fighterSelectButton;
        public FighterEvent fighterSelectedEvent;

        private void Awake()
        {
            for (var i = 0; i < selectableFighters.Count; i++)
            {
                // spawn button
                var fighter = selectableFighters[i];
                var button = Instantiate(fighterSelectButton, viewportContent);
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    fighterSelectedEvent.Broadcast(fighter);
                });
                
                var customizer = button.GetComponentInChildren<TextMeshProTextCustomizer>();
                customizer.text = fighter.fighterName;
            }
        }
    }
}