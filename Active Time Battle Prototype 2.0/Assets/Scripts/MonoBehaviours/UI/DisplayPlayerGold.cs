using System;
using ScriptableObjects.Vars;
using TMPro;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public class DisplayPlayerGold : MonoBehaviour
    {
        public IntVar playerGold;
        public TextMeshProUGUI text;

        public void Update()
        {
            text.text = "gold: " + playerGold.value;
        }
    }
}