using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ItemSystem/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int cost;
        [SerializeField] private string description;
        [SerializeField] private ItemBehaviour[] behaviours;
    }
}