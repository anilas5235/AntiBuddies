using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffectInfo<out TEffect,out TEnum>
    {
        public TEnum GetEffectType();

        public int GetAmount();

        public Color GetColor();

        public TEffect ToEffect(GameObject source);
    }
}