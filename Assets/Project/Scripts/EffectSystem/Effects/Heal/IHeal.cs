using Project.Scripts.EffectSystem.Components;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public interface IHeal : IEffect<IHealable>
    {
        public int CalculateHealing(HealingStats stats, IHealable healable);
    }
}