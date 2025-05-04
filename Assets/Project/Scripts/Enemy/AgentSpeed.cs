using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class AgentSpeed : MonoBehaviour, INeedStatComponent
    {
        [SerializeField] private float baseSpeed = 2f;
        [SerializeField] private StatRef speedStat;

        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;

        public void OnStatInit(StatComponent statComponent)
        {
            speedStat.GetStat(statComponent);
        }

        private void OnEnable()
        {
            speedStat.Stat.OnStatChange += OnSpeedStatChange;
        }

        private void OnSpeedStatChange()
        {
            behaviorGraphAgent.SetVariableValue("Speed", speedStat.Stat.AsFloatPercentage * baseSpeed);
        }
    }
}