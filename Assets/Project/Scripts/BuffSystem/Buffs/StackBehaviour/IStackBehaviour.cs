using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public interface IStackBehaviour
    {
        string Name { get; }
        bool ShouldBuffBeAdded(IBuff buff,BuffManager buffManager);
    }
}