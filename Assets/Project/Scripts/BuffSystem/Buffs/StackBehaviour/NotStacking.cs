namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class NotStacking : IStackBehavior
    {
        public void AddingBuff(IBuff buff)
        {
            if (buff.BuffManager.HasBuff(buff.Name)) return;
            buff.BuffManager.AddBuffToDictionary(buff);
        }
    }
}