using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Unity.Behavior;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class AgentSpeed : MonoBehaviour, INeedStatComponent
    {
        [SerializeField] private ValueStatRef speedStat;

        [SerializeField] private BehaviorGraphAgent behaviorGraphAgent;

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
        }
    }
}