using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumberSpawner : Singleton<FloatingNumberSpawner>
    {
        [SerializeField] private GameObjectPool floatingNumberPool;
        [SerializeField] private float displayDuration = 1.0f;
        [SerializeField] private Vector2 offset = new(0, 0.5f);
        [SerializeField] private bool stopSpawn;

        public void SpawnFloatingNumber(int num, Color color, GameObject source)
        {
            if (stopSpawn) return;
            if (!floatingNumberPool)
            {
                Debug.LogWarning("FloatingNumberPool is null");
                return;
            }

            FloatingNumber numberInstance = (FloatingNumber)floatingNumberPool.GetObject();
            if (!numberInstance)
            {
                Debug.LogWarning("FloatingNumberPool returned null");
                return;
            }

            numberInstance.SetTransform(source.transform.position + (Vector3)offset, Quaternion.identity);
            numberInstance.Setup(new FloatingNumberData(num, color, displayDuration));
        }

        public void SpawnFloatingNumber(int num, DamageType damageType, GameObject source)
        {
            SpawnFloatingNumber(num, damageType.Color, source);
        }

        public void SpawnFloatingNumber(int num, HealType healType, GameObject source)
        {
            SpawnFloatingNumber(num, healType.Color, source);
        }
    }
}