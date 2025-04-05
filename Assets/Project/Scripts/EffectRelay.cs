using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts
{
    public class EffectRelay : MonoBehaviour
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        
        public HealthComponent HealthComponent => healthComponent;
        public AlieGroup AlieGroup => alieGroup;
    }
}
