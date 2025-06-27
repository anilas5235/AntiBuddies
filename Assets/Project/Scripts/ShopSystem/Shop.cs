using System;
using System.Collections.Generic;
using Project.Scripts.ItemSystem;
using Project.Scripts.StatSystem.Stats;
using Project.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.ShopSystem
{
    [DefaultExecutionOrder(-50)]
    public class Shop : Singleton<Shop>
    {
        private const float RerollStep = 1.2f;
        private const float CostStep = 0.20f;
        private const int RerollBase = 3;

        [SerializeField] Item[] commonItems;
        [SerializeField] Item[] rareItems;
        [SerializeField] Item[] epicItems;
        [SerializeField] Item[] legendaryItems;
        [SerializeField] float rareItemChance = 0.2f;
        [SerializeField] float epicItemChance = 0.05f;
        [SerializeField] float legendaryItemChance = 0.01f;
        [SerializeField] int itemCount = 4;
        [SerializeField] float costMultiplier = 1;
        [SerializeField] int rerollCost = RerollBase;
        public int RerollCost => rerollCost;
        public float CostMultiplier => costMultiplier;
        [SerializeField] int maxCommonItems = 10;
        [SerializeField] StatRef luckStat;

        private int _commonItemCount;
        private readonly List<Item> _shopItems = new();

        private void OnEnable()
        {
            luckStat.OnStatInit(GlobalVariables.Instance.PlayerStatGroup);
        }

        public void Reroll()
        {
            if (GlobalVariables.Instance.ResourceManager.HasEnoughGold(rerollCost))
            {
                GlobalVariables.Instance.ResourceManager.RemoveGold(rerollCost);
                GenerateShopItems();
                rerollCost = Mathf.CeilToInt(rerollCost * RerollStep);
            }
        }

        public void GenerateShopItems()
        {
            _shopItems.Clear();

            for (int i = 0; i < itemCount; i++)
            {
                if (_commonItemCount >= maxCommonItems)
                {
                    _commonItemCount = 0;
                    Item slotItem;
                    if (rareItems != null && rareItems.Length > 0)
                        slotItem = rareItems[Random.Range(0, rareItems.Length)];
                    else if (commonItems != null && commonItems.Length > 0)
                        slotItem = commonItems[Random.Range(0, commonItems.Length)];
                    else
                        slotItem = null;
                    _shopItems.Add(slotItem);
                    ShopUI.Instance.SetItem(i, slotItem);
                    _commonItemCount = 0;
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
                    _commonItemCount = 0;
                }
                else if (epicItems != null && epicItems.Length > 0 && randomValue < epicChance + legendaryChance)
                {
                    itemToAdd = epicItems[Random.Range(0, epicItems.Length)];
                    _commonItemCount = 0;
                }
                else if (rareItems != null && rareItems.Length > 0 &&
                         randomValue < rareChance + epicChance + legendaryChance)
                {
                    itemToAdd = rareItems[Random.Range(0, rareItems.Length)];
                    _commonItemCount = 0;
                }
                else if (commonItems != null && commonItems.Length > 0)
                {
                    itemToAdd = commonItems[Random.Range(0, commonItems.Length)];
                    _commonItemCount++;
                }

                // ensure no null: fallback to common pool
                if (itemToAdd == null && commonItems != null && commonItems.Length > 0)
                {
                    itemToAdd = commonItems[Random.Range(0, commonItems.Length)];
                }

                _shopItems.Add(itemToAdd);
                ShopUI.Instance.SetItem(i, itemToAdd);
            }
        }

        public bool BuyItem(Item item)
        {
            if (GlobalVariables.Instance.ResourceManager.HasEnoughGold(item.GetCost(costMultiplier)) &&
                GlobalVariables.Instance.PlayerInventory.HasSpace())
            {
                GlobalVariables.Instance.ResourceManager.RemoveGold(item.GetCost(costMultiplier));
                _shopItems.Remove(item);
                GlobalVariables.Instance.PlayerInventory.Add(item);
                return true;
            }

            return false;
        }

        public void OnShopClose()
        {
            costMultiplier += CostStep;
            rerollCost = Mathf.CeilToInt(costMultiplier * RerollBase);
        }
    }
}