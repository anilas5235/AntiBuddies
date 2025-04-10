namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public interface IHealable
    {
        public void Heal(IHeal amount);
        
        public void FullHeal();
    }
}