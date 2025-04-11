using Project.Scripts.BuffSystem.Data;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public interface IBuff
    {
        void OnBuffAdded();
        void OnBuffTick(float deltaTime);
        void OnBuffApply();
        void OnBuffRemove();
        bool IsBuffExpired();
        void ReduceDuration(float amount);
    }
}