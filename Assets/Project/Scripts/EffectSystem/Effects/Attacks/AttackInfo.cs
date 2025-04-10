using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    [Serializable]
    public class AttackInfo : IEffectInfo<IAttack,AttackType>
    {
        [SerializeField] private int amount;
        [SerializeField] private AttackType attackType;

        public AttackInfo(int amount, AttackType attackType)
        {
            this.amount = amount;
            this.attackType = attackType;
        }
        public AttackType GetEffectType() => attackType;

        public int GetAmount() => amount;

        public Color GetColor() => attackType.GetColor();
        public IAttack ToEffect(GameObject source)
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