using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public abstract class Attack: Effect<IDamageable>
    {
        private readonly float _amount;
        protected Attack(GameObject source, float amount, EffectType effectType) : base(source,effectType)
        {
            _amount = amount;
        }

        public override void Apply(IDamageable target)
        {
            target.TakeDamage(this);
        }
        public abstract int CalculateDamage(ResistanceData resData);


        protected virtual int CalculateDamage(int flatDamageReduction, float resistance)
        {
            float damage = _amount;
            damage -= flatDamageReduction;
            damage *= 1 - resistance;
            return Mathf.RoundToInt(damage);
        }
    }
}