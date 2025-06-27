using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.UI.HUD
{
    /// <summary>
    /// Updates the HUD health bar and health text in response to health changes.
    /// </summary>
    [RequireComponent(typeof(HealthComponent))]
    public class HUDHealth : MonoBehaviour
    {
        /// <summary>
        /// Reference to the HUDVars ScriptableObject for updating UI values.
        /// </summary>
        [SerializeField] private HUDVars hudVars;

        /// <summary>
        /// Reference to the HealthComponent.
        /// </summary>
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            if (!_healthComponent) return;
            // Subscribe to health change event and update display immediately.
            _healthComponent.OnHealthChange += UpdateHealthBar;
            UpdateHealthBar();
        }

        private void OnDisable()
        {
            if (!_healthComponent) return;
            // Unsubscribe from event.
            _healthComponent.OnHealthChange -= UpdateHealthBar;
        }

        /// <summary>
        /// Updates the health bar and health text on the HUD.
        /// </summary>
        private void UpdateHealthBar()
        {
            if (!_healthComponent || !hudVars) return;

            hudVars.Health = _healthComponent.CurrentHealth;
            hudVars.HealthMax = _healthComponent.MaxHealth;
        }
    }
}