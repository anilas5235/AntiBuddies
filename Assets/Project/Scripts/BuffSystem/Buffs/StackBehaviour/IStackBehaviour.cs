using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public interface IStackBehaviour
    {
        string Name { get; }
        void AddingBuff(IBuff buff,BuffManager buffManager);
    }
}