using System;
using System.Collections;
using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.Spawning.Components
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private int spawns = 10;
        [SerializeField] private float initDelay;
        [SerializeField] private float spawnDelay;
        [SerializeField] private bool parentSpawnedObjects;
        [SerializeField] private bool spawnOnEnable = true;
        [SerializeField] private bool DestroyOnFinish = true;
        [SerializeField] protected bool poolingEnabled = true;


        private GameObjectPool _pool;
        private Coroutine _spawnRoutine;
        public bool IsSpawning => _spawnRoutine != null;
        public float SpawnDuration => initDelay + (spawns * spawnDelay);

        protected virtual void OnEnable()
        {
            Init();
            if (spawnOnEnable)
            {
                StartSpawning();
            }
        }

        protected virtual void OnDisable()
        {
            if (IsSpawning)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        private IEnumerator Spawn()
        {
            if (initDelay > 0) yield return new WaitForSeconds(initDelay);
            Vector2 basePos = transform.position;

            for (int i = 0; i < spawns; i++)
            {
                Vector2 spawnPosition = GetSpawnPosition(basePos);
                GameObject spawnedObject = null;

                if (poolingEnabled && _pool)
                {
                    IPoolable pooledObject = _pool.GetObject();
                    if (pooledObject == null)
                    {
                        Debug.LogWarning("Failed to get pooled object from pool for " + prefabToSpawn.name);
                        continue;
                    }

                    pooledObject.SetTransform(spawnPosition, Quaternion.identity);
                    spawnedObject = pooledObject.GetGameObject();
                }
                else
                {
                    spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                }

                if (parentSpawnedObjects)
                {
                    spawnedObject.transform.SetParent(transform);
                }

                if (spawnDelay > 0) yield return new WaitForSeconds(spawnDelay);
            }

            _spawnRoutine = null;
            OnFinishedSpawning();
        }

        // Child classes must implement this to determine spawn positions
        protected abstract Vector2 GetSpawnPosition(Vector2 basePosition);

        public virtual void SetUp(Batch batch)
        {
            if (!batch)
            {
                throw new ArgumentNullException(nameof(batch), "Batch cannot be null");
            }

            spawns = batch.spawnPerBatch;
            prefabToSpawn = batch.enemyPrefab;

            Init();
        }

        private void Init()
        {
            if (poolingEnabled && prefabToSpawn)
            {
                _pool = GlobalPools.Instance.GetPoolFor(prefabToSpawn);
            }
        }

        protected virtual void OnFinishedSpawning()
        {
            if (DestroyOnFinish) Destroy(gameObject);
        }

        public void StartSpawning()
        {
            if (IsSpawning) return;
            _spawnRoutine = StartCoroutine(Spawn());
        }
    }
}