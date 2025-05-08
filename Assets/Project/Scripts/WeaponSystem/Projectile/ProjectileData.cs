using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects.Data;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "WeaponSystem/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public AttackData primaryAttack;
        public DamagingBuffData damagingBuff;
        public StatBuffData statBuff;
        public float speed;
    }
}