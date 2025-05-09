using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "WeaponSystem/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public List<EffectDef<DamageType>> damageEffects;
        public float speed;
    }
}