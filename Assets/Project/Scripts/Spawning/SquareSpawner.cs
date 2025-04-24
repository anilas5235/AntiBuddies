using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawning
{
    public class SquareSpawner : MonoBehaviour
    {
        [SerializeField] private float size = 5f;
        [SerializeField] private int spawns = 10;
        [SerializeField] private GameObject prefabToSpawn;

        private void Awake()
        {
            Spawn();
        }

        private void Spawn()
        {
            float halfSize = size / 2;
            Vector2 basePos = transform.position; // Cache position
            for (int i = 0; i < spawns; i++)
            {
                Vector2 randomPos = new(Random.Range(-halfSize, halfSize), Random.Range(-halfSize, halfSize));
                Vector2 spawnPosition = basePos + randomPos;
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireCube(transform.position, new Vector3(size,size,1));
        }
#endif
    }
}