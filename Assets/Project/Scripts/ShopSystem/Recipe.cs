using Project.Scripts.ItemSystem;
using UnityEngine;

namespace Project.Scripts.ShopSystem
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ShopSystem/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private Item item1;
        [SerializeField] private Item item2;
        [SerializeField] public Item ResultItem { get; private set; }

        public bool IsValid(Item itemA, Item itemB)
        {
            return (itemA == item1 && itemB == item2) || (itemA == item2 && itemB == item1);
        }
    }
}