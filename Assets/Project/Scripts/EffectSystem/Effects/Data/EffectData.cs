using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    public abstract class EffectData<T> : ScriptableObject where T : EffectType
    {
        [SerializeField] protected int Amount;
        [SerializeField] protected T Type;

        public EffectPackage<T> GetPackage(GameObject source, AlieGroup alieGroup,
            Stat percentStat = null, Stat flatStat = null)
        {
            int damage = Amount;
            flatStat?.TransformPositive(damage);
            percentStat?.TransformPositive(damage);

            return new EffectPackage<T>(alieGroup, damage, Type, source);
        }
    }
}