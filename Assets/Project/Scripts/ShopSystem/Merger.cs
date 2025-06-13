using Project.Scripts.ItemSystem;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ShopSystem
{
    public class Merger : MonoBehaviour
    {
        [SerializeField] private Recipe[] recipes;

        public void Merge(Item item1, Item item2)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.IsValid(item1, item2))
                {
                    GlobalVariables.Instance.PlayerInventory.Remove(item1);
                    GlobalVariables.Instance.PlayerInventory.Remove(item2);
                    GlobalVariables.Instance.PlayerInventory.Add(recipe.ResultItem);
                }
            }
        }
    }
}
