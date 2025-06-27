using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.ItemSystem
{
    [Serializable]
    public class ShopElement
    {
        public Visibility visible;
        public int cost;
        public string name;
        public string description;
        public Sprite icon;
        public Color color;

        public bool IsValid => _buyable != null;

        private IBuyable _buyable;

        public ShopElement(IBuyable element, float costMultiplier = 1f)
        {
            if (element == null)
            {
                visible = Visibility.Hidden;
                return;
            }

            visible = Visibility.Visible;
            _buyable = element;
            cost = element.GetCost(costMultiplier);
            name = element.Name;
            description = element.Description;
            icon = element.Icon;
            color = element.Color;
        }

        public void Buy()
        {
            _buyable.Buy();
        }
    }
}