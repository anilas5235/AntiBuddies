using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        
        public HealthComponent HealthComponent => healthComponent;
        public AlieGroup AlieGroup => alieGroup;
    }
}
