using System.Collections.Generic;
using MonoBehaviours.Controllers;
using MonoBehaviours.Customizers;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using ScriptableObjects.RuntimeSets;
using UnityEngine;
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

        public ActionEvent actionSelected;
        public TargetsEvent targetsSelected;

        public void HandleActiveFighter(FighterController fighter)
        {
            var actions = fighter.Actions;

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

            button = Instantiate(buttonPrefab, selectActionUiButtonContainer.transform);
            button.GetComponentInChildren<TextMeshProTextCustomizer>().text.var = "Items";
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                selectItemUi.SetActive(true);

                foreach (var actionButton in selectActionUiButtonContainer.GetComponentsInChildren<Button>())
                    actionButton.interactable = false;
            });

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
                    targetsSelected.Broadcast(appropriateTargets);
                });
            }
            else
            {
                appropriateTargets.ForEach(fighter =>
                {
                    button = Instantiate(buttonPrefab, selectTargetsUiButtonContainer.transform);
                    button.GetComponentInChildren<TextMeshProTextCustomizer>().text = fighter.fighterTemplate.fighterName;
                    button.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        targetsSelected.Broadcast(new List<FighterController> {fighter});
                    });
                });
            }

            selectTargetsUi.SetActive(true);
        }

        private List<FighterController> GetAppropriateTargetsForAction(Action action) =>
            action.healing.Value ? playerFighters.list : enemyFighters.list;
    }
}