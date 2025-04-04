using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;

namespace Project.Scripts.BuffSystem.Components
{
    public abstract class BuffHandler<TBuffType> where TBuffType : IBuff
    {
        private readonly BuffManager _manager;
        private readonly StackBehavior _stackBehavior;

        private readonly List<TBuffType> _buffs;

        protected BuffHandler(BuffManager manager, StackBehavior stackBehavior)
        {
            _manager = manager;
            _stackBehavior = stackBehavior;
        }

        public abstract void AddBuff(TBuffType buff);

        public virtual void UpdateBuffs(float deltaTime)
        {
            List<TBuffType> expiredBuffs = new List<TBuffType>();
            foreach (TBuffType buff in _buffs)
            {
                buff.OnBuffTick(deltaTime);
                if (buff.IsBuffExpired()) expiredBuffs.Add(buff);
            }
            
            RemoveBuffs(expiredBuffs);
        }

        protected void RemoveBuffs(List<TBuffType> buffs)
        {
            foreach (TBuffType expiredBuff in buffs)
            {
                _buffs.Remove(expiredBuff);
            }
        }
    }
}