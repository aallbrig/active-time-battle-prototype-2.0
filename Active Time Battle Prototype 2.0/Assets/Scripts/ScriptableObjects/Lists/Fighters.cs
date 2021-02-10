using System.Collections.Generic;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Lists
{
    [CreateAssetMenu(fileName = "New fighter list", menuName = "ATB/Lists/Fighter", order = 0)]
    public class Fighters : ScriptableObject
    {
        public List<Fighter> list;
    }
}