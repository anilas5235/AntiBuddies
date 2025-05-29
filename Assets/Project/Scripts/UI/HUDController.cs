using Project.Scripts.EffectSystem.Components;
using Project.Scripts.ResourceSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDController : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private GameObject _player;
        private ProgressBar _healthBar;
        private ProgressBar _expBar;
        private Label _goldDisplay;
        
        private HealthComponent _healthComponent;
        private ExpManager _expManager;
        private ResourceManager _resourceManager;

        private void OnEnable()
        {
            if (!_expManager)
            {
                InitFields();
            }

            if (_healthComponent)
            {
                _healthComponent.OnHealthChange += UpdateHealthBar;
                UpdateHealthBar();
            }

            if (_expManager)
            {
                _expManager.OnExpGain += UpdateExpBar;
                _expManager.OnLevelUp += UpdateExpBar;
                UpdateExpBar();
            }
            
            if (_resourceManager)
            {
                _resourceManager.OnGoldChange += UpdateGoldDisplay;
                UpdateGoldDisplay();
            }
        }

        private void OnDisable()
        {
            if (_healthComponent)
            {
                _healthComponent.OnHealthChange -= UpdateHealthBar;
            }

            if (_expManager)
            {
                _expManager.OnExpGain -= UpdateExpBar;
                _expManager.OnLevelUp -= UpdateExpBar;
            }

            if (_resourceManager)
            {
                _resourceManager.OnGoldChange -= UpdateGoldDisplay;
            }
        }


        private void InitFields()
        {
            _expManager = ExpManager.Instance;
            _resourceManager = ResourceManager.Instance;

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
            _goldDisplay = _uiDocument.rootVisualElement.Q<Label>("GoldText");

            if (_healthBar == null || _expBar == null)
            {
                Debug.LogError("Progress bars not found in the UI document.");
            }
        }

        private void UpdateHealthBar()
        {
            _healthBar.value = _healthComponent.HealthPercentage * 100f;
            _healthBar.title = $"{_healthComponent.CurrentHealth}/{_healthComponent.MaxHealth}";
        }

        private void UpdateExpBar()
        {
            _expBar.value = _expManager.ExpProgress * 100f;
            _expBar.title = $"Lvl.{_expManager.Level}";
        }
        private void UpdateGoldDisplay()
        {
            _goldDisplay.text = _resourceManager.Gold.ToString();
        }
    }
}