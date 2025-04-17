using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    [CreateAssetMenu(fileName = "NewAttack", menuName = "EffectsSys/Attack")]
    public class AttackData : BaseEffectData<IAttack>
    {
        [SerializeField] protected AttackType type;

        protected enum AttackType
        {
            Physical,
            Piercing,
            Fire,
            Ice,
            Lightning,
            Poison,
            Bleed,
        }
        
        public override IAttack GetEffect(GameObject source)
        {
            switch (type)
            {
                case AttackType.Physical:
                    return new PhysicalAttack(source, amount);
                case AttackType.Piercing:
                    return new PiercingAttack(source, amount);
                case AttackType.Fire:
                    return new FireAttack(source, amount);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}