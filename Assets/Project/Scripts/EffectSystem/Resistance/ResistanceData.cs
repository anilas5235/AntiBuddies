using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.DamageSystem.Resistance
{
    [Serializable]
    public class ResistanceData
    {
        [SerializeField,Range(0,100)] private int flatDamageReduction;
        [SerializeField,Range(0,1)] private float physicalResistance;
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
        
        public float PhysicalResistance
        {
            get => physicalResistance;
            set => physicalResistance = Mathf.Clamp(value, 0, _maxDamageReduction);
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
    }
}