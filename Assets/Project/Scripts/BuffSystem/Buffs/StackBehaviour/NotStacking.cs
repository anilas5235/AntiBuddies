using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class NotStacking : IStackBehaviour
    {
        private const string ConstName = "NotStacking";
        public string Name => ConstName;
        public bool ShouldBuffBeAdded(IBuff buff, BuffManager buffManager)
        {
            return !buffManager.HasBuff(buff.Name);
        }
    }
}