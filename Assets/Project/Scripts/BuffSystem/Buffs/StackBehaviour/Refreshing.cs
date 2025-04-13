namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class Refreshing : IStackBehavior
    {
        public void AddingBuff(IBuff buff)
        {
            if (buff.BuffManager.TryGetFirstBuff(buff.Name, out IBuff presentBuff))
            {
                presentBuff.Refresh();
            }
            else
            {
                buff.BuffManager.AddBuffToDictionary(buff);
            }
        }
    }
}