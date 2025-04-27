using System.Linq;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "StatSystem/PlayerStats")]
    public class PlayerStats : ScriptableObject, IStatGroup
    {
        [Header("Primary Stats")]
        [Header("Health")]
        public Stat MaxHealth;
        public Stat HealthRegeneration;
        public Stat LifeSteal;
        
        [Header("Offensive")]
        public Stat Damage;
        public Stat MeleeDamage;
        public Stat RangedDamage;
        public Stat AttackSpeed;
        public Stat Range;
        
        [Header("Defensive")]
        public Stat Armor;
        public Stat Shield;
        public Stat Dodage;
        
        [Header("Utility")]
        public Stat MoveSpeed;
        public Stat Luck;
        public Stat Harvesting;

        private Stat[] _stats;
        private void Awake()
        {
            _stats = new[]
            {
                MaxHealth,
                HealthRegeneration,
                LifeSteal,
                Damage,
                MeleeDamage,
                RangedDamage,
                AttackSpeed,
                Range,
                Armor,
                Shield,
                Dodage,
                MoveSpeed,
                Luck,
                Harvesting
            };
        }
        
        public Stat GetStat(StatType statType) => _stats.FirstOrDefault(stat => stat.StatType == statType);
    }
}