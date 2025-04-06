using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    [Serializable]
    public class AttackInfo
    {
        [SerializeField] private int amount;
        [SerializeField] private AttackType attackType;

        public AttackInfo(int amount, AttackType attackType, bool isPercentage = false)
        {
            this.amount = amount;
            this.attackType = attackType;
        }

        public AttackType GetAttackType() => attackType;
        public int GetAmount() => amount;

        public Color GetColor() => attackType.GetColor();

        public Attack ToAttack(GameObject source)
        {
            switch (attackType)
            {
                case AttackType.Physical:
                    return new PhysicalAttack( source, amount);
                case AttackType.Piercing:
                    return new PiercingAttack( source, amount);
                case AttackType.Fire:
                    return new FireAttack( source, amount);
            }
            throw new ArgumentException($"{attackType} effect type is not an a ApplyTo");
        }
    }
}