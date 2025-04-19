namespace Project.Scripts.BuffSystem.Buffs.ExitBehaviour
{
    public class InverseEffectBehaviour : IExitBehaviour
    {
        public void OnExit(IBuff buff)
        {
            buff.OnInverseBuffApply();
        }
    }
}