using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class Stacking : IStackBehaviour
    {
        private const string ConstName = "Stacking";
        public string Name => ConstName;

        public void AddingBuff(IBuff buff, BuffManager buffManager)
        {
            buffManager.AddBuffToDictionary(buff);
        }
    }
}