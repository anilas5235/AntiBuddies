using System.Collections.Generic;
using Project.Scripts.DamageSystem.Components;

namespace Project.Scripts.DamageSystem.Attacks
{
    public struct AttackPackage
    {
        public readonly List<IDamage> AttackDataComponents;
        public readonly DamageSender Sender;

        public AttackPackage(List<IDamage> attackDataComponents, DamageSender sender = null)
        {
            AttackDataComponents = attackDataComponents;
            Sender = sender;
        }

        public AttackPackage(IDamage attackDataComponents, DamageSender sender = null)
            : this(new List<IDamage> { attackDataComponents }, sender)
        {
        }
    }
}