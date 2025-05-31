using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "UI/ShopData")]
    public class ShopData : ScriptableObject
    {
        public ShopElement ShopElement0;
        public ShopElement ShopElement1;
        public ShopElement ShopElement2;
        public ShopElement ShopElement3;
    }
}