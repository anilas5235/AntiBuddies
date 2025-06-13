using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    public class Inventory : MonoBehaviour
    {
        List<Item> items = new ();
        [SerializeField] int maxSize = 10;

    	public void Add(Item item)
        {
			if (items.Count >= maxSize)
            {
                Debug.LogWarning("Inventory is full!");
                return;
            }
            items.Add(item);
            item.OnAdded();
        }

        public void Remove(Item item)
        {
            items.Remove(item);
            item.OnRemoved();
        }

        public void Clear()
        {
            foreach (var item in items)
            {
                item.OnRemoved();
            }
            items.Clear();
        }

        public bool HasSpace()
        {
            return items.Count < maxSize;
        }
    }
}