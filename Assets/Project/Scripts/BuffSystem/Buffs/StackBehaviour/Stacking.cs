namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    public class Stacking : IStackBehavior
    {
        public void AddingBuff(IBuff buff)
        {
            buff.BuffManager.AddBuffToDictionary(buff);
        }
    }
}