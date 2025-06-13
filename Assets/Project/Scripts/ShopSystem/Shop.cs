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
        [SerializeField] int itemCount = 3;
        [SerializeField] int costMultiplier = 1;
        [SerializeField] int rerollCost = 3;
        [SerializeField] int maxCommonItems = 10;
        [SerializeField] StatRef luckStat;
        
        int commonItemCount;
        List<Item> shopItems = new ();

        public void Reroll()
        {
            // if (playerMoney >= rerollCost)
            {
                // playerMoney -= rerollCost;
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
                    shopItems.Add(rareItems[Random.Range(0, rareItems.Length)]);
                    continue;
                }
                float randomValue = Random.Range(0f, 1f);

                float rareChance = rareItemChance * (luckStat.GetValue() + 200) / 200;
                float epicChance = epicItemChance * (luckStat.GetValue() + 200) / 200;
                float legendaryChance = legendaryItemChance * (luckStat.GetValue() + 200) / 200;
                
                if (randomValue < legendaryChance)
                {
                    shopItems.Add(legendaryItems[Random.Range(0, legendaryItems.Length)]);
                    commonItemCount = 0;
                }
                else if (randomValue < epicChance + legendaryChance)
                {
                    shopItems.Add(epicItems[Random.Range(0, epicItems.Length)]);
                    commonItemCount = 0;
                }
                else if (randomValue < rareChance + epicChance + legendaryChance)
                {
                    shopItems.Add(rareItems[Random.Range(0, rareItems.Length)]);
                    commonItemCount = 0;
                }
                else
                {
                    shopItems.Add(commonItems[Random.Range(0, commonItems.Length)]);
                    commonItemCount--;
                }

            }
        }
        public void BuyItem(Item item)
        {
            // if (playerMoney >= item.cost * costMultiplier && GlobalVariables.Instance.PlayerInventory.HasSpace())
            {
                // playerMoney -= item.cost * costMultiplier;
                shopItems.Remove(item);
                GlobalVariables.Instance.PlayerInventory.Add(item);
            }
        }
    }
}
