using System.Collections.Generic;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Data;

namespace Project.Scripts.BuffSystem.Components
{
    public abstract class BuffHandler
    {
        private readonly BuffManager _manager;

        private readonly List<IBuff> _buffs;

        protected BuffHandler(BuffManager manager)
        {
            _manager = manager;
        }

        public abstract void AddBuff(IBuff buff);
        
        public abstract void RemoveBuff(IBuff buff);
    }
}