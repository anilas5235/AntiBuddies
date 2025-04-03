using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    [Serializable]
    public class ActiveBuff
    {
        [SerializeField] private BuffData buffData;
        [SerializeField] private float remainingDuration;
        [SerializeField] private float timeSinceLastTick;
        [SerializeField] private int accumulatedTicks;
        public ActiveBuff(BuffData buffData)
        {
            this.buffData = buffData;
            remainingDuration = buffData.Duration;
        }
        
        public void OnBuffStart()
        {
            accumulatedTicks++;
        }

        public void Tick(float deltaTime)
        {
            remainingDuration -= deltaTime;
            
            if (buffData.TickBehavior != TickBehavior.Ticking || !(timeSinceLastTick >= buffData.TickInterval)) return;
            timeSinceLastTick += deltaTime;
            timeSinceLastTick -= buffData.TickInterval;
            accumulatedTicks++;
        }
        
        public void OnBuffEnd()
        {
            
        }
        
        public void Refresh()
        {
            if (buffData.StackBehavior == StackBehavior.Refresh) remainingDuration = buffData.Duration;
        }
        
        public List<EffectInfo> GetEffect() => buffData.Effects.ToList();

        public bool IsExpired() => remainingDuration <= 0;

        public int PickUpAccumulatedTicks()
        {
            int ticks = accumulatedTicks;
            accumulatedTicks = 0;
            return ticks;
        }
    }
}