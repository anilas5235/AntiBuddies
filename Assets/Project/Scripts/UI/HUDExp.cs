using Project.Scripts.ResourceSystem;
using UnityEngine;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(ExpManager))]
    public class HUDExp : MonoBehaviour
    {
        [SerializeField] private HUDVars hudVars;
        private ExpManager _expManager;

        private void Awake()
        {
            _expManager = GetComponent<ExpManager>();
        }

        private void OnEnable()
        {
            if (_expManager)
            {
                _expManager.OnExpGain += UpdateExpBar;
                _expManager.OnLevelUp += UpdateExpBar;
            }
        }

        private void OnDisable()
        {
            if (_expManager)
            {
                _expManager.OnExpGain -= UpdateExpBar;
                _expManager.OnLevelUp -= UpdateExpBar;
            }
        }

        private void UpdateExpBar()
        {
            if (!_expManager || !hudVars) return;
            hudVars.Level = _expManager.Level;
            hudVars.levelBar = _expManager.ExpProgress * 100f;
        }
    }
}