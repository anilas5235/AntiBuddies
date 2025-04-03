using System;
using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class BuffHandler : MonoBehaviour
    {
        public List<IBuff> ActiveBuffs = new();

        public void AddBuff(BuffData buffData)
        {
            //TODO: Check if the buff can be added based on the stack behavior
        }

        private void FixedUpdate()
        {
            List<IBuff> expiredBuffs = new();
            
            foreach (IBuff buff in ActiveBuffs)
            {
                buff.OnBuffApply();
                LifeCycle(buff, expiredBuffs);
            }
            
            RemoveBuffs(expiredBuffs);
        }
        
        private void RemoveBuffs(List<IBuff> buffs)
        {
            foreach (IBuff expiredBuff in buffs)
            {
                ActiveBuffs.Remove(expiredBuff);
            }
        }

        private static void LifeCycle(IBuff buff, List<IBuff> expiredBuffs)
        {
            buff.OnBuffTick(Time.deltaTime);
            if (buff.IsBuffExpired()) expiredBuffs.Add(buff);
        }
    }
}