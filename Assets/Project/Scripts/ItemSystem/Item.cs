using System;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ItemSystem/Item")]
    public class Item : ScriptableObject, IBuyable
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int cost;
        [SerializeField] private string description;
        [SerializeField] private ItemBehaviour[] behaviours;


        public int Cost => cost;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;
        public Color Color => Color.gray; // TODO: Implement color logic
        public void Buy()
        {
            //TODO: Implement the logic for buying the item
        }
        
        public void OnRemoved()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnRemoved();
            }
        }

        public void OnAdded()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnAdded();
            }
        }
    }
}