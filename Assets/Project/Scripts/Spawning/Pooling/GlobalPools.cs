using Project.Scripts.Utils;
using Project.Scripts.WeaponSystem.Projectile;
using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public class GlobalPools : Singleton<GlobalPools>
    {
        [SerializeField] private GameObjectPool<ProjectileData> projectilePool = new();
        
        public GameObjectPool<ProjectileData>  ProjectilePool => projectilePool;

        protected override void Awake()
        {
            base.Awake();
            InitializePools();
        }

        private void InitializePools()
        {
            projectilePool.Init();
        }
    }
}