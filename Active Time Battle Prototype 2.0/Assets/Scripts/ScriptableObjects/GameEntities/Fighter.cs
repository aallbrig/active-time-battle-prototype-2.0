using ScriptableObjects.Refs;
using UnityEngine;

namespace ScriptableObjects.GameEntities
{
    [CreateAssetMenu(fileName = "New fighter", menuName = "ATB/Fighter", order = 0)]
    public class Fighter : ScriptableObject
    {
        [Header("Fighter specific")] public StringRef fighterName;

        public GameObjectRef model;
        public RuntimeAnimatorController animator;
        public FighterClass fighterClass;
        public IntRef cost;
        public IntRef goldRewardMin;
        public IntRef goldRewardMax;
    }
}