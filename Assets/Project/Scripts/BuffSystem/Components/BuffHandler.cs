using System.Collections.Generic;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Data;

namespace Project.Scripts.BuffSystem.Components
{
    public abstract class BuffHandler<TTarget>
    {
        private readonly BuffManager _manager;

        private readonly List<Buff<TTarget>> _buffs;

        protected BuffHandler(BuffManager manager)
        {
            _manager = manager;
        }

        public abstract void AddBuff(IBuff<TTarget> buff);
        
        public abstract void RemoveBuff(IBuff<TTarget> buff);
    }
}