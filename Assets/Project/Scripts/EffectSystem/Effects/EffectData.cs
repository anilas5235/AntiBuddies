using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [CreateAssetMenu(fileName = "NewEffect", menuName = "EffectsSys/Effect")]
    public class EffectData : ScriptableObject
    {
        [SerializeField] protected int amount;
        [SerializeField] protected EffectType effectType;

        public EffectPackage GetPackage(GameObject source, AlieGroup alieGroup)
        {
            return new EffectPackage(alieGroup, amount, effectType, source);
        }
    }
}