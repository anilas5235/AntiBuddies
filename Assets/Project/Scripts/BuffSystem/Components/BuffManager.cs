using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class BuffManager : MonoBehaviour
    {
        private readonly Dictionary<string,List<IBuff>> _buffs = new();

        public void AddBuff(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return;
            }
            buff.AddBuff(this);
        }
        
        internal void AddBuffToDictionary([NotNull]IBuff buff)
        {
            string key = buff.Name;
            if (_buffs.TryGetValue(key, out List<IBuff> buffList))
            {
                buffList.Add(buff);
            }
            else
            {
                _buffs.Add(key,new List<IBuff>{buff});
            }
            buff.OnBuffAdded();
        }
        
        public void RemoveBuff(IBuff buff)
        {
           
        }

        internal bool TryGetFirstBuff(string typeName, out IBuff buff)
        {
            buff = null;
            if (!_buffs.TryGetValue(typeName, out List<IBuff> buffList)) return false;
            if (buffList.Count <= 0) return false;
            buff = buffList.First();
            return true;
        }

        internal int GetBuffCount(string typeName)
        {
            return _buffs.TryGetValue(typeName, out List<IBuff> buffList) ? buffList.Count : 0;
        }
     
        internal bool HasBuff(string typeName) => GetBuffCount(typeName) > 0;
        
        public void ClearAllBuffs()
        {
            foreach (IBuff buff in _buffs.Values.SelectMany(buffList => buffList))
            {
                buff.OnBuffRemove();
            }

            _buffs.Clear();
        }
        
        public void ClearBuffs(string typeName)
        {
            if (!_buffs.TryGetValue(typeName, out List<IBuff> buffList)) return;
            
            foreach (IBuff buff in buffList)
            {
                buff.OnBuffRemove();
            }
            _buffs.Remove(typeName);
        }

        internal void RemoveBuffFromDictionary(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return;
            }
            
            buff.OnBuffRemove();
            string key = buff.Name;
            if (_buffs.TryGetValue(key, out List<IBuff> buffList))
            {
                buffList.Remove(buff);
            }
        }
    }
}