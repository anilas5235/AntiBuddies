using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "ItemStatBehaviour", menuName = "ItemSystem/StatBehaviour")]
    public class ItemStatBehaviour : ItemBehaviour
    {
        [SerializeField] private StatData statData;
        public override void OnAdded(EffectRelay effectRelay)
        {
            effectRelay.Apply(statData.GetPackage(null, AlieGroup.Neutral));
            
        }

        public override void OnRemoved(EffectRelay effectRelay)
        {
            effectRelay.Apply(statData.GetPackage(null, AlieGroup.Neutral).Invert());
        }
    }
}