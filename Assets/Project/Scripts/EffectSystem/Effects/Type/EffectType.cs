using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    public abstract class EffectType : ScriptableObject
    {
        [SerializeField] private string description = "no description jet";
        public string Name => name;
        public string Description => description;
        
        [SerializeField] private StatType scaleStat;
        public int Scale(int amount, StatComponent statComponent)
        {
            if (scaleStat) amount = statComponent.GetStat(scaleStat).TransformPositive(amount);
            return amount;
        }
    }
}