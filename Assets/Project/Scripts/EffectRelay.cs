using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts
{
    public class EffectRelay : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        
        public HealthComponent HealthComponent => healthComponent;
    }
}
