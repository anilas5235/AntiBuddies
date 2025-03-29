using Project.Scripts.DamageSystem.Attacks;

namespace Project.Scripts.DamageSystem.Resistance
{
    public interface IResistance
    {
        float GetResistance(DamageType damageType);
        int GetFlatDamageReduction(DamageType damageType);
    }
}