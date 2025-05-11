using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class BuffManager : MonoBehaviour
    {
        private readonly Dictionary<string, List<IBuff>> _buffs = new();

        public void AddBuff(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return;
            }

            if(!buff.ShouldBuffBeAdded(this)) return;
            CentralBuffTicker.Instance.RegisterBuff(buff);
            AddBuffToDictionary(buff);
        }

        private void AddBuffToDictionary([NotNull] IBuff buff)
        {
            string key = buff.Name;
            if (_buffs.TryGetValue(key, out List<IBuff> buffList))
            {
                buffList.Add(buff);
            }
            else
            {
                _buffs.Add(key, new List<IBuff> { buff });
            }

            buff.OnBuffAdded();
        }
        
        public void RemoveBuff(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return;
            }

            RemoveBuffFromDictionary(buff);
        }
        
        internal void RemoveBuffFromDictionary(IBuff buff)
        {
            if (!_buffs.TryGetValue(buff.Name, out List<IBuff> buffList)) return;
            buffList.Remove(buff);
        }
       
        internal bool TryGetFirstBuff(string typeName, out IBuff buff)
        {
            buff = null;
            if (!_buffs.TryGetValue(typeName, out List<IBuff> buffList)) return false;
            if (buffList.Count <= 0) return false;
            buff = buffList.First();
            return true;
        }

        public int GetBuffCount(string typeName)
        {
            return _buffs.TryGetValue(typeName, out List<IBuff> buffList) ? buffList.Count : 0;
        }

        public bool HasBuff(string typeName) => GetBuffCount(typeName) > 0;
    }
}