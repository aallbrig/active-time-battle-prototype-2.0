using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public class CharacterSelectPanelContainerUi : MonoBehaviour
    {
        private readonly List<CharacterSelectUi> _selectionPanels = new List<CharacterSelectUi>();

        private void Start() => _selectionPanels.AddRange(GetComponentsInChildren<CharacterSelectUi>());

        public void OnAllRandomClick() => _selectionPanels.ForEach(panel => panel.OnRandomizeClick());
    }
}