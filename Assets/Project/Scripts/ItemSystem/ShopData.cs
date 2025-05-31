using Project.Scripts.ResourceSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "UI/ShopData")]
    public class ShopData : ScriptableObject
    {
        public ShopElement ShopElement0;
        public ShopElement ShopElement1;
        public ShopElement ShopElement2;
        public ShopElement ShopElement3;

        public void Clear()
        {
            ShopElement0 = null;
            ShopElement1 = null;
            ShopElement2 = null;
            ShopElement3 = null;
        }

        public void SetItem(int index, Item item)
        {
            switch (index)
            {
                case 0:
                    ShopElement0 = new ShopElement(item);
                    break;
                case 1:
                    ShopElement1 = new ShopElement(item);
                    break;
                case 2:
                    ShopElement2 = new ShopElement(item);
                    break;
                case 3:
                    ShopElement3 = new ShopElement(item);
                    break;
                default:
                    Debug.LogWarning("Invalid index for shop element: " + index);
                    break;
            }
        }

        public void Get(int index, out ShopElement shopElement)
        {
            shopElement = null;
            switch (index)
            {
                case 0:
                    shopElement = ShopElement0;
                    break;
                case 1:
                    shopElement = ShopElement1;
                    break;
                case 2:
                    shopElement = ShopElement2;
                    break;
                case 3:
                    shopElement = ShopElement3;
                    break;
                default:
                    Debug.LogWarning("Invalid index for shop element: " + index);
                    break;
            }
        }

        public void BuyItem(int index)
        {
            Get(index, out ShopElement shopElement);
            if (shopElement.IsValid && ResourceManager.Instance.HasEnoughGold(shopElement.cost))
            {
                shopElement.Buy();
                SetItem(index, null); // Clear the item after buying
            }
            else
            {
                Debug.LogWarning($"Cannot buy item at index {index}: Item is not visible or does not exist.");
            }
        }
    }
}