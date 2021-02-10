using System.Collections.Generic;
using ScriptableObjects.Refs;
using UnityEngine;

namespace ScriptableObjects.GameEntities
{
    [CreateAssetMenu(fileName = "New fighter class", menuName = "ATB/FighterClass", order = 0)]
    public class FighterClass : ScriptableObject
    {
        public IntRef minHp;
        public IntRef maxHp;
        public IntRef minMp;
        public IntRef maxMp;
        public FloatRef secondsUntilBattleMeterFull;
        public List<Action> actions;
    }
}