using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    public interface IProjectile : IPoolable, IHandleContact

    {
        public void SetData(ProjectileData projectileData, DamagePackage damagePackage, DamageBuff damageBuff);
        public void ProjectileSetUp(Vector2 direction, AlieGroup alieGroup, int contacts);
    }
}