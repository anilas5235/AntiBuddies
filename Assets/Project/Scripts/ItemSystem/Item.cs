using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ItemSystem/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int cost;
        [SerializeField] private string description;
        [SerializeField] private ItemBehaviour[] behaviours;
        [SerializeField] private Sprite icon;
        [SerializeField] private Rarity rarity;
        
        public void OnAdded()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnAdded();
            }
        }
        
        public void OnRemoved()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnRemoved();
            }
        }
        
        public enum Rarity
        {
            Common,
            Rare,
            Epic,
            Legendary,
            Crafted
        }
    }
}