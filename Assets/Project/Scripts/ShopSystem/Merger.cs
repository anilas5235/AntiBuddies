using Project.Scripts.ItemSystem;
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
                    // Item1.behaviours().OnRemoved(PlayerEffectRelay);
                    // Item2.behaviours().OnRemoved(PlayerEffectRelay);
                    // recipe.GetResultItem().behaviours().OnAdded(PlayerEffectRelay);
                    // Add item to inventory
                }
            }
        }
    }
}
