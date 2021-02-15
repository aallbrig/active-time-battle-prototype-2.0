using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using ScriptableObjects.Events;
using ScriptableObjects.GameEntities;
using UnityEngine;
using UnityEngine.AI;

namespace MonoBehaviours.Controllers
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Fighter))]
    public class FighterController : MonoBehaviour, IHealable, IDamageable
    {
        public FighterControllerEvent actionStart;
        public FighterControllerEvent actionComplete;
        public FighterControllerEvent fighterDied;
        public ActionEffectEvent fighterDamaged;
        public ActionEffectEvent fighterHealed;
        public Fighter fighterTemplate;
        public int maxHp;
        public int currentHp;
        public int maxMp;
        public int currentMp;
        public float battleMeterTickPerSecond;
        public float currentBattleMeter;
        public bool ready = true;
        private NavMeshAgent _agent;
        private GameObject _model;
        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        private Transform _transform;

        public List<Action> Actions => fighterTemplate.fighterClass.actions;

        public void Reset()
        {
            ResetBattleMeter();
            _transform.position = _startingPosition;
            _transform.rotation = _startingRotation;
            _agent.SetDestination(_startingPosition);
        }

        private void Start()
        {
            _transform = transform;
            _startingPosition = _transform.position;
            _startingRotation = _transform.rotation;
            _agent = GetComponent<NavMeshAgent>();

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

        public void Damage(int hurt)
        {
            fighterDamaged.Broadcast(this, hurt);
            currentHp = Mathf.Clamp(currentHp - hurt, 0, maxHp);
            if (currentHp <= 0) fighterDied.Broadcast(this);
        }
        public void Heal(int heal)
        {
            fighterHealed.Broadcast(this, heal);
            if (currentHp <= 0 && heal > 0) ResetBattleMeter();

            currentHp = Mathf.Clamp(currentHp + heal, 0, maxHp);
        }

        public void BattleMeterTick(float deltaTime)
        {
            if (currentBattleMeter >= 1.0f) return;

            currentBattleMeter = Mathf.Clamp(currentBattleMeter + battleMeterTickPerSecond * deltaTime, 0f, 1.0f);
        }

        public void ResetBattleMeter() => currentBattleMeter = 0f;

        public IEnumerator PerformAction(Action action, List<FighterController> targets)
        {
            if (currentHp <= 0 || targets.Count == 0 || targets.Where(fighter => fighter.currentHp > 0).ToList().Count <= 0)
            {
                actionComplete.Broadcast(this);
                yield break;
            }

            while (targets.Where(fighter => !fighter.ready).ToList().Count > 0)
                yield return null;

            actionStart.Broadcast(this);
            ready = false;

            // Ingress
            _agent.stoppingDistance = action.stoppingDistance.Value;
            _agent.destination = FindCenterPoint(targets);
            yield return AgentReachedDestination(_agent);

            // Action
            yield return action.Act(this, targets);

            // Egress
            _agent.stoppingDistance = 0;
            _agent.destination = _startingPosition;
            yield return AgentReachedDestination(_agent);
            _transform.rotation = _startingRotation;

            actionComplete.Broadcast(this);
            ready = true;
        }

        public void HandleActionComplete(FighterController fighter)
        {
            if (fighter == this)
                ResetBattleMeter();
        }

        private IEnumerator AgentReachedDestination(NavMeshAgent agent)
        {
            while (true)
            {
                if (!agent.pathPending)
                    if (agent.remainingDistance <= agent.stoppingDistance + 1)
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            // Agent has reached destination
                            break;

                yield return null;
            }
        }

        private Vector3 FindCenterPoint(List<FighterController> targets) =>
            targets.Select(target => target.gameObject.transform.position)
                .Aggregate(new Vector3(), (acc, position) => acc += position) / targets.Count;
    }
}