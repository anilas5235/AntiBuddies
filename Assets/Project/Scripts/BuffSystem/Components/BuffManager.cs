using System.Collections.Generic;
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
            
            string key = buff.Name;
            if (_buffs.TryGetValue(key, out List<IBuff> buffList))
            {
                buffList.Add(buff);
            }
            else
            {
                _buffs.Add(key,new List<IBuff>{buff});
            }
            buff.BuffManager = this;
            buff.OnBuffAdded();
        }
        
        public void RemoveBuff(IBuff buff)
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