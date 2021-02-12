using System.Collections.Generic;
using System.Linq;
using MonoBehaviours.Controllers;
using MonoBehaviours.Customizers;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.RuntimeSets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MonoBehaviours.UI
{
    public class GameRunningUi : MonoBehaviour
    {
        public FighterControllerRuntimeSet playerFighters;
        public FighterControllerRuntimeSet enemyFighters;
        public GameObject selectActionUi;
        public GameObject selectActionUiButtonContainer;
        public GameObject selectItemUi;
        public GameObject selectItemUiButtonContainer;
        public GameObject selectTargetsUi;
        public GameObject selectTargetsUiButtonContainer;
        public GameObject buttonPrefab;

        public FighterControllerEvent onTargetHoverEnter;
        public FighterControllerEvent onTargetHoverLeave;
        public ActionEvent actionSelected;
        public TargetsEvent targetsSelected;

        private FighterController _activeFighter;

        public void UnsetActiveFighter() => _activeFighter = null;
        public void HandleFighterDie(FighterController fighter)
        {
            if (fighter == _activeFighter)
            {
                selectActionUi.SetActive(false);
                selectItemUi.SetActive(false);
                selectTargetsUi.SetActive(false);
            }
        }

        public void HandleActiveFighter(FighterController fighter)
        {
            _activeFighter = fighter;
            var actions = _activeFighter.Actions;

            foreach (Transform child in selectActionUiButtonContainer.transform)
                Destroy(child.gameObject);

            GameObject button;
            actions.ForEach(action =>
            {
                button = Instantiate(buttonPrefab, selectActionUiButtonContainer.transform);
                button.GetComponentInChildren<TextMeshProTextCustomizer>().text = action.actionName;
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    actionSelected.Broadcast(action);

                    foreach (var actionButton in selectActionUiButtonContainer.GetComponentsInChildren<Button>())
                        actionButton.interactable = false;
                });
            });

            // button = Instantiate(buttonPrefab, selectActionUiButtonContainer.transform);
            // button.GetComponentInChildren<TextMeshProTextCustomizer>().text.var = "Items";
            // button.GetComponent<Button>().onClick.AddListener(() =>
            // {
            //     selectItemUi.SetActive(true);

            //     // TODO: Handle Items
            //     // foreach (var actionButton in selectActionUiButtonContainer.GetComponentsInChildren<Button>())
            //     // actionButton.interactable = false;
            // });

            selectActionUi.SetActive(true);
        }

        public void HandleActionSelected(Action action)
        {
            var appropriateTargets = GetAppropriateTargetsForAction(action);

            foreach (Transform child in selectTargetsUiButtonContainer.transform)
                Destroy(child.gameObject);

            GameObject button;

            if (action.multiple.Value)
            {
                button = Instantiate(buttonPrefab, selectTargetsUiButtonContainer.transform);
                button.GetComponentInChildren<TextMeshProTextCustomizer>().text.var = "All";
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    appropriateTargets.ForEach(onTargetHoverLeave.Broadcast);
                    targetsSelected.Broadcast(appropriateTargets);
                });

                // I can't believe I can't just add button.onHover.AddListener!
                var pointerEnterEventEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
                pointerEnterEventEntry.callback.AddListener(eventData =>
                    appropriateTargets.ForEach(onTargetHoverEnter.Broadcast));

                var pointerLeaveEventEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
                pointerLeaveEventEntry.callback.AddListener(eventData =>
                    appropriateTargets.ForEach(onTargetHoverLeave.Broadcast));

                button.gameObject.AddComponent<EventTrigger>();
                button.gameObject.GetComponent<EventTrigger>().triggers.Add(pointerEnterEventEntry);
                button.gameObject.GetComponent<EventTrigger>().triggers.Add(pointerLeaveEventEntry);
            }
            else
            {
                appropriateTargets.ForEach(fighter =>
                {
                    button = Instantiate(buttonPrefab, selectTargetsUiButtonContainer.transform);
                    button.GetComponentInChildren<TextMeshProTextCustomizer>().text = fighter.fighterTemplate.fighterName;
                    button.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        onTargetHoverLeave.Broadcast(fighter);
                        targetsSelected.Broadcast(new List<FighterController> {fighter});
                    });

                    // I can't believe I can't just add button.onHover.AddListener!
                    var pointerEnterEventEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
                    pointerEnterEventEntry.callback.AddListener(eventData => onTargetHoverEnter.Broadcast(fighter));

                    var pointerLeaveEventEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
                    pointerLeaveEventEntry.callback.AddListener(eventData => onTargetHoverLeave.Broadcast(fighter));

                    button.gameObject.AddComponent<EventTrigger>();
                    button.gameObject.GetComponent<EventTrigger>().triggers.Add(pointerEnterEventEntry);
                    button.gameObject.GetComponent<EventTrigger>().triggers.Add(pointerLeaveEventEntry);
                });
            }

            selectTargetsUi.SetActive(true);
        }

        private List<FighterController> GetAppropriateTargetsForAction(Action action) =>
            action.healing.Value ? playerFighters.list : enemyFighters.list.Where(fighter => fighter.currentHp > 0).ToList();
    }
}