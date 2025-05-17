using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    internal interface IApplyEffect
    {
        public int Apply(GameObject other, AlieGroup alieGroup);
    }
}