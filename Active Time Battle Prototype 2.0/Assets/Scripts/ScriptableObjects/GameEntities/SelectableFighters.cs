using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.GameEntities
{
    [CreateAssetMenu(fileName = "Selectable Fighters", menuName = "ATB/SelectableFighters", order = 0)]
    public class SelectableFighters : ScriptableObject
    {
        public List<Fighter> fighters;
    }
}