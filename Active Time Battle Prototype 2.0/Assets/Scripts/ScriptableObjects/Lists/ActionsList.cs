using System.Collections.Generic;
using ScriptableObjects.GameEntities;
using UnityEngine;

namespace ScriptableObjects.Lists
{
    [CreateAssetMenu(fileName = "New action list", menuName = "ATB/Lists/Actions", order = 0)]
    public class ActionsList : ScriptableObject
    {
        public List<Action> list;
    }
}