using Project.Scripts.ResourceSystem;
using UnityEngine;

namespace Project.Scripts.UI.HUD
{
    /// <summary>
    /// Updates the HUD gold display in response to gold changes.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class HUDGold : MonoBehaviour
    {
        /// <summary>
        /// Reference to the HUDVars ScriptableObject for updating UI values.
        /// </summary>
        [SerializeField] private HUDVars hudVars;

        /// <summary>
        /// Reference to the ResourceManager component.
        /// </summary>
        private ResourceManager _resourceManager;

        private void Awake()
        {
            _resourceManager = GetComponent<ResourceManager>();
        }

        private void OnEnable()
        {
            if (!_resourceManager) return;
            // Subscribe to gold change event and update display immediately.
            _resourceManager.OnGoldChange += UpdateGoldDisplay;
            UpdateGoldDisplay();
        }

        private void OnDisable()
        {
            if (!_resourceManager) return;
            // Unsubscribe from event.
            _resourceManager.OnGoldChange -= UpdateGoldDisplay;
        }

        /// <summary>
        /// Updates the gold value on the HUD.
        /// </summary>
        private void UpdateGoldDisplay()
        {
            if (!_resourceManager || !hudVars) return;
            hudVars.gold = _resourceManager.Gold;
        }
    }
}