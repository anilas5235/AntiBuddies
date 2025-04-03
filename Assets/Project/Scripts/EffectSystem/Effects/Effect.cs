using Project.Scripts.DamageSystem.Attacks;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class Effect<S,T>
    {
        protected readonly S Source;

        private readonly EffectType _effectType;
        protected Effect(S source, EffectType effectType)
        {
            Source = source;
            _effectType = effectType;
        }
        
        public abstract void Apply(T target);
        
        public EffectType GetEffectType() => _effectType;
    }
}