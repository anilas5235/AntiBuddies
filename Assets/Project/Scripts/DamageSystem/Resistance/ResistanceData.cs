using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Resistance
{
    [Serializable]
    public class ResistanceData
    {
        [SerializeField,Range(0,100)] private int flatDamageReduction;
        [SerializeField,Range(0,1)] private float damageReduction;
        [SerializeField,Range(0,1)] private float piercingResistance;
        [SerializeField,Range(0,1)] private float fireResistance;
        
        private float _maxFlatDamageReduction = 100f;
        private float _maxDamageReduction = 1f;
        private float _maxPiercingResistance = 1f;
        private float _maxFireResistance = 1f;
        
        public int FlatDamageReduction
        {
            get => flatDamageReduction;
            set => flatDamageReduction = Mathf.Clamp(value, 0, (int)_maxFlatDamageReduction);
        }
        
        public float DamageReduction
        {
            get => damageReduction;
            set => damageReduction = Mathf.Clamp(value, 0, _maxDamageReduction);
        }
        
        public float PiercingResistance
        {
            get => piercingResistance;
            set => piercingResistance = Mathf.Clamp(value, 0, _maxPiercingResistance);
        }
        
        public float FireResistance
        {
            get => fireResistance;
            set => fireResistance = Mathf.Clamp(value, 0, _maxFireResistance);
        }

        public float GetResistance(EffectType effectType)
        {
            return effectType switch
            {
                EffectType.Physical => damageReduction,
                EffectType.Piercing => piercingResistance,
                EffectType.Fire => fireResistance,
                _ => throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null)
            };
        }

        public int GetFlatReduction(EffectType effectType)
        {
            return flatDamageReduction;
        }
    }
}