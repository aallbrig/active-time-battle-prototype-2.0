using ScriptableObjects.Events;
using ScriptableObjects.Lists;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI
{
    public class SelectionPanelUi : MonoBehaviour
    {
        public Fighters selectableFighters;
        public Transform viewportContent;
        public GameObject fighterSelectionCard;
        public FighterEvent fighterSelectedEvent;

        private void Awake()
        {
            selectableFighters.list.ForEach(fighter =>
            {
                var card = Instantiate(fighterSelectionCard, viewportContent);
                card.GetComponent<FighterCard>().Fighter = fighter;
                card.GetComponentInChildren<Button>().onClick.AddListener(() => fighterSelectedEvent.Broadcast(fighter));
            });
        }
    }
}