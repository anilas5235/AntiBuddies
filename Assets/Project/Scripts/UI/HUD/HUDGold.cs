using Project.Scripts.ResourceSystem;
using UnityEngine;

namespace Project.Scripts.UI.HUD
{
    public class HUDGold : MonoBehaviour
    {
        [SerializeField] private HUDVars hudVars;
        private ResourceManager _resourceManager;

        private void Awake()
        {
            _resourceManager = GetComponent<ResourceManager>();
        }

        private void OnEnable()
        {
            if (_resourceManager)
            {
                _resourceManager.OnGoldChange += UpdateGoldDisplay;
                UpdateGoldDisplay();
            }
        }

        private void OnDisable()
        {
            if (_resourceManager)
            {
                _resourceManager.OnGoldChange -= UpdateGoldDisplay;
            }
        }

        private void UpdateGoldDisplay()
        {
            if (!_resourceManager || !hudVars) return;
            hudVars.gold = _resourceManager.Gold;
        }
    }
}