using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.UI.HUD
{
    [RequireComponent(typeof(HealthComponent))]
    public class HUDHealth : MonoBehaviour
    {
        [SerializeField] private HUDVars hudVars;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            if (_healthComponent)
            {
                _healthComponent.OnHealthChange += UpdateHealthBar;
                UpdateHealthBar();
            }
        }

        private void OnDisable()
        {
            if (_healthComponent)
            {
                _healthComponent.OnHealthChange -= UpdateHealthBar;
            }
        }

        private void UpdateHealthBar()
        {
            if (!_healthComponent || !hudVars) return;

            hudVars.Health = _healthComponent.CurrentHealth;
            hudVars.HealthMax = _healthComponent.MaxHealth;
        }
    }
}