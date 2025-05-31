using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class Enemy : PoolableMono, INeedStatComponent
    {
        [SerializeField] private ValueStatRef speedStat;
        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;
        private StatComponent _statComponent;

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
            speedStat.Init(_statComponent);
        }

        private void OnEnable()
        {
            speedStat.OnValueChange += OnSpeedStatChange;
            OnSpeedStatChange();
            behaviorGraphAgent.Restart();
        }

        private void OnDisable()
        {
            speedStat.OnValueChange -= OnSpeedStatChange;
        }

        private void OnSpeedStatChange()
        {
            behaviorGraphAgent.SetVariableValue("Speed", speedStat.CurrValue);
        }

        public void Die()
        {
            ReturnToPool();
        }

        private void OnValidate()
        {
            speedStat.UpdateValue();
        }

        public override void Reset()
        {
        }
    }
}