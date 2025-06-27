using System;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ItemSystem/Item")]
    public class Item : ScriptableObject, IBuyable
    {
        public enum Rarity
        {
            Common,
            Rare,
            Epic,
            Legendary,
            Crafted
        }

        [SerializeField] private Sprite icon;
        [SerializeField] private int cost;
        [SerializeField] private ItemBehaviour[] behaviours;
        [SerializeField] public Rarity rarity;


        public int Cost => cost;
        public string Name => name;
        public string Description => this.ToString();
        public Sprite Icon => icon;
        public Color Color => RarityColor(rarity);

        public int GetCost(float costMultiplier) => Mathf.CeilToInt(cost * costMultiplier);

        public void Buy()
        {
        }

        public void OnRemoved()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnRemoved();
            }
        }

        public void OnAdded()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnAdded();
            }
        }

        private static Color RarityColor(Rarity rarity)
        {
            Color color = Color.gray;
            switch (rarity)
            {
                case Rarity.Common:
                    color = Color.gray;
                    break;
                case Rarity.Rare:
                    color = new Color(0.12f, 0.18f, 0.37f); // blue
                    break;
                case Rarity.Epic:
                    color = new Color(0.31f, 0f, 0.31f); // Purple
                    break;
                case Rarity.Legendary:
                    color = new Color(0.61f, 0.61f, 0f); // Yellow
                    break;
                case Rarity.Crafted:
                    color = new Color(0.5f, 0.25f, 0); // Brown
                    break;
            }

            return color;
        }

        public override string ToString()
        {
            String str = "";
            foreach (var itemBehaviour in behaviours)
            {
                str += itemBehaviour.ToString();
            }

            return str;
        }
    }
}