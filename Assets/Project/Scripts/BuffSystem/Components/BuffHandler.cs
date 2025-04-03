using System;
using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class BuffHandler : MonoBehaviour
    {
        public List<ActiveBuff> activeBuffs = new();

        public void AddBuff(BuffData buffData)
        {
            if(!buffData) return;
            ActiveBuff newBuff = new(buffData);
            activeBuffs.Add(newBuff);
            newBuff.OnBuffStart();
        }

        private void FixedUpdate()
        {
            List<ActiveBuff> expiredBuffs = new();
            List<EffectInfo> effects = new();
            
            foreach (ActiveBuff buff in activeBuffs)
            {
                GetBuffEffects(buff, effects);
                LifeCycle(buff, expiredBuffs);
            }
            
            RemoveBuffs(expiredBuffs);
        }
        
        private void RemoveBuffs(List<ActiveBuff> buffs)
        {
            foreach (ActiveBuff expiredBuff in buffs)
            {
                activeBuffs.Remove(expiredBuff);
            }
        }

        private static void LifeCycle(ActiveBuff buff, List<ActiveBuff> expiredBuffs)
        {
            buff.Tick(Time.deltaTime);
            if (buff.IsExpired()) expiredBuffs.Add(buff);
        }

        private static void GetBuffEffects(ActiveBuff buff, List<EffectInfo> effects)
        {
            int ticks = buff.PickUpAccumulatedTicks();
            if (ticks <= 0) return;
            List<EffectInfo> effect = buff.GetEffect();
            for (int i = 0; i < ticks; i++)
            {
                effects.AddRange(effect);
            }
        }
    }
}