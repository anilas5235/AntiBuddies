using System;
using Project.Scripts.EffectSystem.Components;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDController:MonoBehaviour
    {
        private UIDocument _uiDocument;
        private GameObject _player;
        private ProgressBar _healthBar;
        private ProgressBar _expBar;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            if (!_player)
            {
                Debug.LogError("Player not found.");
                return;
            }
            _healthComponent = _player.GetComponent<HealthComponent>();
            _uiDocument = GetComponent<UIDocument>();
           
            _healthBar = _uiDocument.rootVisualElement.Q<ProgressBar>("Health");
            _expBar = _uiDocument.rootVisualElement.Q<ProgressBar>("Exp");
        }

        private void FixedUpdate()
        {
            _healthBar.value = _healthComponent.HealthPercentage * 100f;
            _healthBar.title = $"{_healthComponent.CurrentHealth}/{_healthComponent.MaxHealth}";
        }
    }
}