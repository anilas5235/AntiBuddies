using Project.Scripts.BuffSystem.Data;

namespace Project.Scripts.BuffSystem.Components
{
    public interface IBuff
    {
        StackBehavior StackBehavior { get; }
        
        void OnBuffAdded();
        void OnBuffTick(float deltaTime);
        void OnBuffApply();
        void OnBuffRemove();
        bool IsBuffExpired();
    }
}