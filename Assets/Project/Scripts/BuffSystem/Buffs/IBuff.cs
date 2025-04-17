using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Components;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public interface IBuff
    {
        GameObject Source { get; }
        BuffManager BuffManager { get; }      
        string Name { get; }
        void OnBuffAdded();
        void OnBuffTick(float deltaTime);
        void OnBuffApply();
        void OnBuffRemove();
        bool IsBuffExpired();
        void ReduceDuration(float amount);
        void RegisteredAtBuffManager(BuffManager buffManager);
        void RegisteredAtBuffGroup(BuffGroup buffGroup);
        void Refresh();
        void RemoveBuff();
    }
}