using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class BaseEffectData<T> : ScriptableObject, IEffectData<T>
    {
        [SerializeField] protected int amount;
        
        public abstract T GetEffect(GameObject source);
    }
}