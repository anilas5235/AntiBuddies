using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class Enemy : PoolableMono, INeedStatGroup
    {
        [SerializeField] private ValueStatRef speedStat;
        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;
        private IStatGroup _statGroup;

        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
            speedStat.OnStatInit(_statGroup);
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