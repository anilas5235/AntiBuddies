using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Enemy
{
    public class AgentSpeed : MonoBehaviour, INeedStatComponent
    {
        [SerializeField] private float baseSpeed = 2f;
        [SerializeField] private StatRef speedStat;

        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;
        [SerializeField] private NavMeshAgent navMeshAgent;

        public void OnStatInit(StatComponent statComponent)
        {
            speedStat.GetStat(statComponent);
        }

        private void OnEnable()
        {
            speedStat.Stat.OnStatChange += OnSpeedStatChange;
            OnSpeedStatChange();
        }

        private void OnSpeedStatChange()
        {
            float speed = speedStat.Stat.AsFloatPercentage * baseSpeed;
            behaviorGraphAgent.SetVariableValue("Speed",speed);
            navMeshAgent.speed = speed;
        }
    }
}