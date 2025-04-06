using Project.Scripts.EffectSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public abstract class Attack : Effect<IDamageable>
    {
        private readonly float _amount;
        private readonly AttackType _attackType;
        public AttackType AttackType => _attackType;

        protected Attack(GameObject source, float amount, AttackType attackType) : base(source)
        {
            _amount = amount;
            _attackType = attackType;
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
            damage = Mathf.Max(damage, 0);
            return Mathf.RoundToInt(damage);
        }
    }
}