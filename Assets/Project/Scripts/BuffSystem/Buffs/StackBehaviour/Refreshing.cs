using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class Refreshing : IStackBehaviour
    {
        private const string ConstName = "Refreshing";

        public string Name => ConstName;

        public void AddingBuff(IBuff buff, BuffManager buffManager)
        {
            if (buffManager.TryGetFirstBuff(buff.Name, out IBuff presentBuff))
            {
                presentBuff.Refresh();
            }
            else
            {
                buffManager.AddBuffToDictionary(buff);
            }
        }
    }
}