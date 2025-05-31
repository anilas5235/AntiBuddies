using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Project.Scripts.ItemSystem
{
    [Serializable]
    public class ShopElement
    {
        public Visibility visible;
        public int cost;
        public string description;
        public Sprite icon;
        public Color color;
        
        private IBuyable _buyable;
        
        public ShopElement(IBuyable element)
        {
            if (element == null)
            {
                visible = Visibility.Hidden;
                return;
            }
            visible = Visibility.Visible;
            _buyable = element;
            cost = element.Cost;
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