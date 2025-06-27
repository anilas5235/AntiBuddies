using System;
using Project.Scripts.ItemSystem;
using Project.Scripts.ResourceSystem;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.Utils
{
    [DefaultExecutionOrder(-60)]
    public class GlobalVariables : Singleton<GlobalVariables>
    {
        public GameObject Player { get; private set; }
        public IStatGroup PlayerStatGroup { get; private set; }
        public Inventory PlayerInventory { get; private set; }
        public ResourceManager ResourceManager { get; private set; }

        public event Action OnWaveStart;
        public event Action OnWaveEnd;

        public void TriggerWaveStart()
        {
            OnWaveStart?.Invoke();
        }

        public void TriggerWaveEnd()
        {
            OnWaveEnd?.Invoke();
        }

        protected override void Awake()
        {
            base.Awake();
            Player = GameObject.FindGameObjectWithTag("Player");
            if (Player == null)
            {
                Debug.LogError("Player not found in the scene.");
                return;
            }

            PlayerStatGroup = Player.GetComponent<IStatGroup>();
            if (PlayerStatGroup == null)
            {
                Debug.LogError("PlayerStatGroup not found in the scene.");
                return;
            }

            PlayerInventory = Player.GetComponent<Inventory>();
            if (PlayerInventory == null)
                Debug.LogError("PlayerInventory component not found on Player.");
            ResourceManager = ResourceManager.Instance;
        }
    }
}