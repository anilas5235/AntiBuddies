using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// CentralBuffTicker is a singleton class that manages the central buff ticker in the game.
    /// </summary>
    public class CentralBuffTicker : MonoBehaviour
    {
        private static CentralBuffTicker _instance;
        private const float TickInterval = 0.1f;
        public BuffGroup DamageBuffGroup;
        public BuffGroup HealBuffGroup;
        public BuffGroup StatusBuffGroup;


        public static CentralBuffTicker Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = FindFirstObjectByType<CentralBuffTicker>(); // Try to find an existing instance in the scene
                if (_instance) return _instance;
                GameObject obj = new("CentralBuffTicker"); // If no instance is found, create a new one
                _instance = obj.AddComponent<CentralBuffTicker>();
                return _instance;
            }
        }

        private Coroutine _coroutine;

        private void Awake()
        {
            if (_instance && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                InitBuffGroups();
            }
        }
        
        private void InitBuffGroups()
        {
            DamageBuffGroup = gameObject.AddComponent<BuffGroup>();
            DamageBuffGroup.Name = "Damage Buff Group";
            HealBuffGroup = gameObject.AddComponent<BuffGroup>();
            HealBuffGroup.Name = "Heal Buff Group";
            StatusBuffGroup = gameObject.AddComponent<BuffGroup>();
            StatusBuffGroup.Name = "Status Buff Group";
        }
    }
}
