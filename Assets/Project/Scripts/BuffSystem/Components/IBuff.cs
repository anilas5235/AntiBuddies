namespace Project.Scripts.BuffSystem.Components
{
    public interface IBuff
    {
        public void OnBuffAdded();
        public void OnBuffTick(float deltaTime);
        public void OnBuffApply();
        public void OnBuffRemove();

        public void OnBuffRefresh();
        
        public bool IsBuffExpired();
    }
}