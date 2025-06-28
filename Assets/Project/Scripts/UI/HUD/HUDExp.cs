using Project.Scripts.ResourceSystem;
using UnityEngine;

namespace Project.Scripts.UI.HUD
{
    /// <summary>
    /// Updates the HUD experience bar and level display in response to experience gain and level up events.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [RequireComponent(typeof(ExpManager))]
    public class HUDExp : MonoBehaviour
    {
        /// <summary>
        /// Reference to the HUDVars ScriptableObject for updating UI values.
        /// </summary>
        [SerializeField] private HUDVars hudVars;

        /// <summary>
        /// Reference to the ExpManager component.
        /// </summary>
        private ExpManager _expManager;

        private void Awake()
        {
            _expManager = GetComponent<ExpManager>();
        }

        private void OnEnable()
        {
            if (!_expManager) return;
            // Subscribe to experience gain and level up events.
            _expManager.OnExpGain += UpdateExpBar;
            _expManager.OnLevelUp += UpdateExpBar;
            UpdateExpBar(); // Initial update to set the current values.
        }

        private void OnDisable()
        {
            if (!_expManager) return;
            // Unsubscribe from events.
            _expManager.OnExpGain -= UpdateExpBar;
            _expManager.OnLevelUp -= UpdateExpBar;
        }

        /// <summary>
        /// Updates the experience bar and level display on the HUDVars ScriptableObject.
        /// </summary>
        private void UpdateExpBar()
        {
            if (!_expManager || !hudVars) return;
            hudVars.Level = _expManager.Level;
            hudVars.levelBar = _expManager.ExpProgress * 100f;
        }
    }
}