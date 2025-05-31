using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectPipeline : MonoBehaviour, INeedStatComponent
    {
        private readonly List<IEffectPipelineComponent> _components = new();
        private IStatGroup _statGroup;


        public void AddComponent(IEffectPipelineComponent component)
        {
            if (component == null) return;
            _components.Add(component);
        }

        public void RemoveComponent(IEffectPipelineComponent component)
        {
            if (component == null) return;
            _components.Remove(component);
        }

        public void Execute(IPackageHub hub, EffectPipelineMode mode)
        {
            foreach (IEffectPipelineComponent component in _components)
            {
                if (!component.ShouldAdd(mode)) continue;
                component.ApplyTo(hub, _statGroup);
            }
        }

        public void ClearEffects()
        {
            _components.Clear();
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statGroup = statComponent;
        }
    }
}