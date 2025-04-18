using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [CreateAssetMenu(fileName = "NewEffect", menuName = "EffectsSys/Effect")]
    public class EffectData : ScriptableObject
    {
        [SerializeField] protected int amount;
        [SerializeField] protected EffectType effectType;
        
        public EffectPackage GetPackage(GameObject source, AlieGroup alieGroup) => new(alieGroup,amount, effectType, source);
    }
}