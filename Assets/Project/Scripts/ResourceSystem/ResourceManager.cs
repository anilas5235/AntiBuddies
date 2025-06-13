using System;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [SerializeField] private int gold;
        
        public int Gold => gold;
        
        public bool HasEnoughGold(int amount) => gold >= amount;
        public event Action OnGoldChange;
        
        public void AddGold(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative gold.");
                return;
            }
            gold += amount;
            OnGoldChange?.Invoke();
        }
        
        public void RemoveGold(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot remove negative gold.");
                return;
            }
            if (gold < amount)
            {
                Debug.LogWarning("Not enough gold to remove. Setting amount to current gold.");
                amount = gold;
            }
            gold -= amount;
            OnGoldChange?.Invoke();
        }

    }
}