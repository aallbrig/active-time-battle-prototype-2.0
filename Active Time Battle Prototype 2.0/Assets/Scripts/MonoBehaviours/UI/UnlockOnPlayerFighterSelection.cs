using ScriptableObjects.RuntimeSets;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI
{
    public class UnlockOnPlayerFighterSelection : MonoBehaviour
    {
        public FighterRuntimeSet playerSelectedFighters;
        public Button button;
        private void Update() => button.interactable = playerSelectedFighters.list.Count > 0;
    }
}