using Project.Scripts.DamageSystem.Attacks;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class Effect<S,T>
    {
        protected readonly S Source;
        protected readonly T Target;

        private readonly EffectType _effectType;
        protected Effect(T target, S source, EffectType effectType)
        {
            Source = source;
            Target = target;
            _effectType = effectType;
        }
        
        public abstract void Apply();
        
        public EffectType GetEffectType() => _effectType;
    }
}