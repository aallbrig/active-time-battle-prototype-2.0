using System.Collections.Generic;
using ScriptableObjects.GameEntities;
using UnityEngine;
using UnityEngine.AI;

namespace MonoBehaviours.Controllers
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Fighter))]
    public class FighterController : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent agent;
        public Fighter fighterTemplate;
        public int maxHp;
        public int currentHp;
        public int maxMp;
        public int currentMp;
        public float battleMeterTickPerSecond;

        public List<Action> Actions => fighterTemplate.fighterClass.actions;

        private Transform _transform;
        private GameObject _model;

        private void Start()
        {
            _transform = transform;
            agent = GetComponent<NavMeshAgent>();

            maxHp = Random.Range(fighterTemplate.fighterClass.minHp.Value, fighterTemplate.fighterClass.maxHp.Value);
            currentHp = maxHp;
            maxMp = Random.Range(fighterTemplate.fighterClass.minMp.Value, fighterTemplate.fighterClass.maxMp.Value);
            currentMp = maxMp;
            battleMeterTickPerSecond = 1 / fighterTemplate.fighterClass.secondsUntilBattleMeterFull.Value;

            _model = Instantiate(fighterTemplate.model.Value, _transform);

            if (fighterTemplate.animator != null)
            {
                var animator = _model.GetComponent<Animator>();
                if (animator == null) animator = _model.AddComponent<Animator>();

                animator.runtimeAnimatorController = fighterTemplate.animator;
            }
        }
    }
}