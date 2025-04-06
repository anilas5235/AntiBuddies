namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public interface IHealable
    {
        public void Heal(int amount);
        
        public void FullHeal();
    }
}