using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;

namespace Project.Scripts.BuffSystem.Components
{
    public class DamagingBuff : Buff<IDamageable>
    {
        protected Attack Attack;
        
        public DamagingBuff(BuffData buffData, IDamageable target,IDamageDealer source) : base(buffData, target)
        {
            Attack = buffData.Effect.ToAttack(source);
        }


        protected override void ExecuteEffect()
        {
            target?.TakeDamage(Attack);
        }
    }
}