namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public interface IHealable : ITarget<IHeal>
    {
        public void FullHeal();
    }
}