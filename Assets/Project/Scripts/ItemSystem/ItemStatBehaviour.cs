using Project.Scripts.StatSystem.Stats;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "ItemStatBehaviour", menuName = "ItemSystem/StatBehaviour")]
    public class ItemStatBehaviour : ItemBehaviour
    {
        [SerializeField] StatModification[] statModification;
        
        public override void OnAdded()
        {
            foreach (var modification in statModification)
            {
                GlobalVariables.Instance.PlayerStatGroup.ModifyStat(modification);
            }
        }

        public override void OnRemoved()
        {
            foreach (var modification in statModification)
            {
                GlobalVariables.Instance.PlayerStatGroup.ModifyStat(modification.Inverse());
            }
        }
    }
}