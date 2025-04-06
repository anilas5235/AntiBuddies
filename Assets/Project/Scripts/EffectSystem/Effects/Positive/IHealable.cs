namespace Project.Scripts.EffectSystem.Effects.Positive
{
    public interface IHealable
    {
        public void Heal(int amount);
        
        public void FullHeal();
    }
}