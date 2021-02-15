using ScriptableObjects.Refs;
using ScriptableObjects.Vars;
using UnityEngine;

namespace MonoBehaviours.Utils
{
    public class SetPlayerStartingGold : MonoBehaviour
    {
        public IntRef startingGold;
        public IntVar playerGold;

        // This happens only once
        private void Start() => playerGold.value = startingGold.Value;
    }
}