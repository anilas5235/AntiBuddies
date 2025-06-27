using Project.Scripts.ItemSystem;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ShopSystem
{
    public class Merger : MonoBehaviour
    {
        [SerializeField] private Recipe[] recipes;

        public Recipe[] Recipes => recipes;

        public void Merge(Item item1, Item item2)
        {
            Debug.Log($"Attempting to merge {item1.name} and {item2.name}");
            bool merged = false;
            for (int i = 0; i < recipes.Length; i++)
            {
                var recipe = recipes[i];
                bool valid = recipe.IsValid(item1, item2);
                Debug.Log($"Checking recipe '{recipe.name}' valid: {valid}");
                if (valid)
                {
                    GlobalVariables.Instance.PlayerInventory.Remove(item1);
                    GlobalVariables.Instance.PlayerInventory.Remove(item2);
                    GlobalVariables.Instance.PlayerInventory.Add(recipe.ResultItem);
                    Debug.Log($"Merge successful: created {recipe.ResultItem.name}");
                    merged = true;
                    break;
                }
            }
            if (!merged)
                Debug.Log($"No valid recipe found for {item1.name} and {item2.name}");
        }
    }
}
