using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    [CreateAssetMenu(fileName = "NewStatus", menuName = "EffectsSys/Status")]
    public class StatusData : BaseEffectData<IStatus>
    {
        [SerializeField] protected StatusType statusType;
        protected enum StatusType : byte
        {
            Stun,
            Slow,
            Weak,
            ArmorBreaking,
            Berserk,
            Vulnerable,
        }
        
        public override IStatus GetEffect(GameObject source)
        {
            return statusType switch
            {
                StatusType.Stun => new Stun(source, amount),
                StatusType.Slow => new Slow(source, amount),
                StatusType.Weak => new Weak(source, amount),
                StatusType.ArmorBreaking => new ArmorBreaking(source, amount),
                StatusType.Berserk => new Berserk(source, amount),
                StatusType.Vulnerable => new Vulnerable(source, amount),
                _ => throw new System.NotImplementedException($"Status type {statusType} is not implemented.")
            };
            
        }
    }
}