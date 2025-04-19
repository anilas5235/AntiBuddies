using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class NotStacking : IStackBehaviour
    {
        private const string ConstName = "NotStacking";
        public string Name => ConstName;

        public void AddingBuff(IBuff buff, BuffManager buffManager)
        {
            if (buffManager.HasBuff(buff.Name)) return;
            buffManager.AddBuffToDictionary(buff);
        }
    }
}