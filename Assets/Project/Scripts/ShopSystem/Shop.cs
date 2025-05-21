using System.Collections.Generic;
using Project.Scripts.ItemSystem;
using UnityEngine;

namespace Project.Scripts.ShopSystem
{
    public class Shop : MonoBehaviour
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
        [SerializeField] private int rerollCost = 3;
        
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
                float randomValue = Random.Range(0f, 1f);

                if (randomValue < legendaryItemChance)
                {
                    shopItems.Add(legendaryItems[Random.Range(0, legendaryItems.Length)]);
                    commonItemCount = 0;
                }
                else if (randomValue < epicItemChance + legendaryItemChance)
                {
                    shopItems.Add(epicItems[Random.Range(0, epicItems.Length)]);
                    commonItemCount = 0;
                }
                else if (randomValue < rareItemChance + epicItemChance + legendaryItemChance)
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
            // if (playerMoney >= item.cost * costMultiplier)
            {
                // playerMoney -= item.cost * costMultiplier;
                // item.behaviours().OnAdded(PlayerEffectRelay);
                // shopItems.Remove(item);
                // Add item to inventory
            }
        }
    }
}
