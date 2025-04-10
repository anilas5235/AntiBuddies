using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public interface IStatus : IEffect<IStatusEffectable>
    {
       StatusType StatusType { get; }
    }
}