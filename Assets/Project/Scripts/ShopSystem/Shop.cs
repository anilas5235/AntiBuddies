using System.Collections.Generic;
using Project.Scripts.ItemSystem;
using Project.Scripts.StatSystem.Stats;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ShopSystem
{
    public class Shop : Singleton<Shop>
    {
        [SerializeField] Item[] commonItems;
        [SerializeField] Item[] rareItems;
        [SerializeField] Item[] epicItems;
        [SerializeField] Item[] legendaryItems;
        [SerializeField] float rareItemChance = 0.2f;
        [SerializeField] float epicItemChance = 0.05f;
        [SerializeField] float legendaryItemChance = 0.01f;
        [SerializeField] int itemCount = 4;
        [SerializeField] int costMultiplier = 1;
        [SerializeField] int rerollCost = 3;
        public int RerollCost => rerollCost;
        [SerializeField] int maxCommonItems = 10;
        [SerializeField] StatRef luckStat;
        
        int commonItemCount;
        List<Item> shopItems = new ();

        public void Reroll()
        {
            if (GlobalVariables.Instance.ResourceManager.HasEnoughGold(rerollCost))
            {
                GlobalVariables.Instance.ResourceManager.RemoveGold(rerollCost);
                GenerateShopItems();
            }
        }
        public void GenerateShopItems()
        {
            shopItems.Clear();

            for (int i = 0; i < itemCount; i++)
            {
                 if (commonItemCount >= maxCommonItems)
                 {
                     commonItemCount = 0;
                     Item slotItem;
                     if (rareItems != null && rareItems.Length > 0)
                         slotItem = rareItems[Random.Range(0, rareItems.Length)];
                     else if (commonItems != null && commonItems.Length > 0)
                         slotItem = commonItems[Random.Range(0, commonItems.Length)];
                     else
                         slotItem = null;
                     shopItems.Add(slotItem);
                     ShopUI.Instance.SetItem(i, slotItem);
                     commonItemCount = 0;
                     continue;
                 }
                 float randomValue = Random.Range(0f, 1f);

                 float rareChance = rareItemChance * (luckStat.GetValue() + 200) / 200;
                 float epicChance = epicItemChance * (luckStat.GetValue() + 200) / 200;
                 float legendaryChance = legendaryItemChance * (luckStat.GetValue() + 200) / 200;
                
                Item itemToAdd = null;
                if (legendaryItems != null && legendaryItems.Length > 0 && randomValue < legendaryChance)
                {
                    itemToAdd = legendaryItems[Random.Range(0, legendaryItems.Length)];
                    commonItemCount = 0;
                }
                else if (epicItems != null && epicItems.Length > 0 && randomValue < epicChance + legendaryChance)
                {
                    itemToAdd = epicItems[Random.Range(0, epicItems.Length)];
                    commonItemCount = 0;
                }
                else if (rareItems != null && rareItems.Length > 0 && randomValue < rareChance + epicChance + legendaryChance)
                {
                    itemToAdd = rareItems[Random.Range(0, rareItems.Length)];
                    commonItemCount = 0;
                }
                else if (commonItems != null && commonItems.Length > 0)
                {
                    itemToAdd = commonItems[Random.Range(0, commonItems.Length)];
                    commonItemCount++;
                }
                // ensure no null: fallback to common pool
                if (itemToAdd == null && commonItems != null && commonItems.Length > 0)
                {
                    itemToAdd = commonItems[Random.Range(0, commonItems.Length)];
                }
                shopItems.Add(itemToAdd);
                ShopUI.Instance.SetItem(i, itemToAdd);
            }
        }
        public bool BuyItem(Item item)
        {
            if (GlobalVariables.Instance.ResourceManager.HasEnoughGold(item.Cost * costMultiplier) && GlobalVariables.Instance.PlayerInventory.HasSpace())
            {
                GlobalVariables.Instance.ResourceManager.RemoveGold(item.Cost * costMultiplier);
                shopItems.Remove(item);
                GlobalVariables.Instance.PlayerInventory.Add(item);
                return true;
            }
            return false;
        }
    }
}
