using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Enemy
{
    public class AgentSpeed : MonoBehaviour, INeedStatComponent
    {
        [SerializeField] private ValueStatRef speedStat;

        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;
        [SerializeField] private NavMeshAgent navMeshAgent;

        public void OnStatInit(StatComponent statComponent)
        {
            speedStat.Init(statComponent);
        }

        private void OnEnable()
        {
            speedStat.Stat.OnStatChange += OnSpeedStatChange;
            OnSpeedStatChange();
        }

        private void OnSpeedStatChange()
        {
            behaviorGraphAgent.SetVariableValue("Speed",speedStat.CurrValue);
            navMeshAgent.speed = speedStat.CurrValue;
        }
    }
}