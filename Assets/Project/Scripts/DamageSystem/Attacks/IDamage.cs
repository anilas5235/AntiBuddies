using Project.Scripts.DamageSystem.Resistance;

namespace Project.Scripts.DamageSystem.Attacks
{
    public interface IDamage
    {
        public int CalcDamage(IResistance resistance);
        
        public DamageType GetDamageType();
        
        public int GetDamage();
    }
}