using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    public abstract class EffectType : ScriptableObject
    {
        [SerializeField] private string description = "no description jet";
        [SerializeField] private StatType flatScaleStat;
        [SerializeField] private StatType percentageScaleStat;
        public string Name => name;
        public string Description => description;
        public StatType FlatScaleStat => flatScaleStat;
        public StatType PercentageScaleStat => percentageScaleStat;
    }
}