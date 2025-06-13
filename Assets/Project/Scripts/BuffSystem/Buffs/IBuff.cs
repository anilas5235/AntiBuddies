using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;

namespace Project.Scripts.BuffSystem.Buffs
{
    public interface IBuff
    {
        BuffManager BuffManager { get; }
        BuffGroup BuffGroup { get; set; }
        public IPackageHub Hub { get; set; }
        bool AffectsAllies { get; }
        string Name { get; }
        void OnBuffAdded();
        void OnBuffTick(float deltaTime);
        void OnBuffApply();
        void OnBuffRemove();
        bool IsBuffExpired();
        void ReduceDuration(float amount);
        bool ShouldBuffBeAdded(BuffManager buffManager);
        void Refresh();
        void RemoveBuff();
        IBuff GetCopy();
    }
}