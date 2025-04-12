using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    [Serializable]
    public class Batch
    {
        public GameObject enemyPrefab;
        public int spawnPerBatch;
        public float spawnDelay;
        public int numberOfBatches;
        public float spawnRadius;
        public float initialDelay;
    }

    [Serializable]
    public class Wave
    {
        public List<Batch> batches = new();
        public float timeBetweenWaves;
    }
}

