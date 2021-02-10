using System.Collections.Generic;
using MonoBehaviours.Customizers;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.Lists;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI
{
    public class SelectionPanelUi : MonoBehaviour
    {
        public Fighters selectableFighters;
        public Transform viewportContent;
        public GameObject fighterSelectButton;
        public FighterEvent fighterSelectedEvent;

        private void Awake()
        {
            for (var i = 0; i < selectableFighters.list.Count; i++)
            {
                // spawn button
                var fighter = selectableFighters.list[i];
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