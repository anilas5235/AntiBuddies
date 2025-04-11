using Project.Scripts.BuffSystem.Buffs;

namespace Project.Scripts.BuffSystem.Components
{
    public class StatBuffHandler : BuffHandler
    {
        public StatBuffHandler(BuffManager manager) : base(manager)
        {
        }

        public override void AddBuff(IBuff buff)
        {
            CentralBuffTicker.Instance.StatusBuffGroup.RegisterBuff(buff);
        }

        public override void RemoveBuff(IBuff buff)
        {
            CentralBuffTicker.Instance.StatusBuffGroup.UnregisterBuff(buff);
        }
    }
}