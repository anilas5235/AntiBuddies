using System;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    public class ShopUI : MonoBehaviour
    {
        public ShopData Data;
        public Item Item0;

        private void Start()
        {
            Data.ShopElement0 = new ShopElement(Item0);
        }
    }
}