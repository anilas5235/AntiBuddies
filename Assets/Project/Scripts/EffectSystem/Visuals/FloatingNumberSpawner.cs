using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumberSpawner : Singleton<FloatingNumberSpawner>
    {
        [SerializeField] private GameObject damageNumberPrefab;
        [SerializeField] private float displayDuration = 1.0f;
        [SerializeField] private Vector2 offset = new(0, 0.5f);
        [SerializeField] private bool stopSpawn;
        
        public void SpawnFloatingNumber(int num,Color color, GameObject source)
        {
            if (!damageNumberPrefab) return;

            FloatingNumber numberInstance =
                Instantiate(damageNumberPrefab, source.transform.position + (Vector3)offset, Quaternion.identity)
                    .GetComponent<FloatingNumber>();

            numberInstance.Setup(new FloatingNumberData(num, color, displayDuration));
        }

        public void SpawnFloatingNumber(int num,AttackType attackType, GameObject source)
        {
            SpawnFloatingNumber(num, attackType.Color, source);
        }
        
        public void SpawnFloatingNumber(int num,HealType healType ,GameObject source)
        {
            SpawnFloatingNumber(num, healType.Color, source);
        }
    }
}