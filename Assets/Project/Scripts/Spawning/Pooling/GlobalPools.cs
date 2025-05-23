using System;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public class GlobalPools : Singleton<GlobalPools>
    {
        [SerializeField] private GameObjectPool projectilePool;
        [SerializeField] private GameObjectPool puddlePool;


        protected override void Awake()
        {
            base.Awake();
            InitializePools();
        }

        private void InitializePools()
        {
            projectilePool.Init();
        }

        public GameObjectPool GetPoolFor(AvailablePool type)
        {
            return type switch
            {
                AvailablePool.Projectile => projectilePool,
                AvailablePool.Puddle => puddlePool,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}