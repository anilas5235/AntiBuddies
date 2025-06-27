using Project.Scripts.ItemSystem;
using UnityEngine;

namespace Project.Scripts.ShopSystem
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ShopSystem/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private Item item1;
        [SerializeField] private Item item2;
        [SerializeField] private Item resultItem;

        public bool IsValid(Item itemA, Item itemB)
        {
            return (itemA == item1 && itemB == item2) || (itemA == item2 && itemB == item1);
        }

        public bool IsPresent(Item item)
        {
            return item1 == item || item2 == item;
        }

        public Item ResultItem => resultItem;
    }
}