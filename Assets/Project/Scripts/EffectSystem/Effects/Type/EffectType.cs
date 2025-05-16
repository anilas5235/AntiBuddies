using System.Collections.Generic;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    public abstract class EffectType : ScriptableObject
    {
        [SerializeField] private string description = "no description jet";
        public string Name => name;
        public string Description => description;

        public virtual int CreationScale(int amount, StatComponent statComponent, List<IStat> extraStats)
        {
            return amount;
        }
        
        public virtual int ReceptionScale(int amount, StatComponent statComponent, List<IStat> extraStats)
        {
            return amount;
        }
    }
}
