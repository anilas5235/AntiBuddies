using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    /// <summary>
    /// Represents an enemy entity with stat and behavior graph integration.
    /// </summary>
    public class Enemy : PoolableMono, INeedStatGroup
    {
        /// <summary>
        /// Reference to the speed stat for this enemy.
        /// </summary>
        [SerializeField] private ValueStatRef speedStat;
        /// <summary>
        /// The behavior graph agent controlling this enemy's AI.
        /// </summary>
        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;
        /// <summary>
        /// The stat group associated with this enemy.
        /// </summary>
        private IStatGroup _statGroup;

        /// <inheritdoc/>
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

        /// <summary>
        /// Called when the speed stat value changes; updates the behavior graph variable.
        /// </summary>
        private void OnSpeedStatChange()
        {
            behaviorGraphAgent.SetVariableValue("Speed", speedStat.CurrValue);
        }

        /// <summary>
        /// Handles the enemy's death and returns it to the pool.
        /// </summary>
        public void Die()
        {
            ReturnToPool();
        }

        private void OnValidate()
        {
            // Ensure the speed stat is represented correctly in the editor.
            speedStat.UpdateValue();
        }

        /// <inheritdoc/>
        public override void Reset()
        {
        }
    }
}