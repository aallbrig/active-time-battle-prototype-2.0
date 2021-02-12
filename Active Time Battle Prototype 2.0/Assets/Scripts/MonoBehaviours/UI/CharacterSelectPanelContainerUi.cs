using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public class CharacterSelectPanelContainerUi : MonoBehaviour
    {
        private List<CharacterSelectUi> _selectionPanels = new List<CharacterSelectUi>();

        public void OnAllRandomClick() => _selectionPanels.ForEach(panel => panel.OnRandomizeClick());
        
        private void Start()
        {
            _selectionPanels.AddRange(GetComponentsInChildren<CharacterSelectUi>());
        }
    }
}