using Project.Scripts.EffectSystem.Components;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    internal interface IApplyDynamicEffect
    {
        public int Apply(GameObject other, AllyGroup allyGroup, IStatGroup statGroup, GameObject source);
    }
}