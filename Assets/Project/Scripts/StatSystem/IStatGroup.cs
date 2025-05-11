using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;

namespace Project.Scripts.StatSystem
{
    public interface IStatGroup
    {
        IStat GetStat(StatType statType);
    }
}