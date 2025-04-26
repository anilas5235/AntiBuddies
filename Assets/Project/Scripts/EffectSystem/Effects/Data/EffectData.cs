using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    public abstract class EffectData<T> : ScriptableObject where T:EffectType
    {
        [SerializeField] protected int Amount;
        [SerializeField] protected T Type;

        public EffectPackage<T> GetPackage(GameObject source, AlieGroup alieGroup)
        {
            return new EffectPackage<T>(alieGroup, Amount, Type, source);
        }
    }
}