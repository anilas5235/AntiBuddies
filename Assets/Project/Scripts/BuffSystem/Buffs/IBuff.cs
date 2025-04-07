using Project.Scripts.BuffSystem.Data;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public interface IBuff<out TTarget>
    {
        StackBehavior StackBehavior { get; }
        TTarget Target { get; }
        
        GameObject Source { get; }
        
        void OnBuffAdded();
        void OnBuffTick(float deltaTime);
        void OnBuffApply();
        void OnBuffRemove();
        bool IsBuffExpired();
    }
}