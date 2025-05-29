using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    [CreateAssetMenu(fileName = "Wave", menuName = "SpawnSystem/Wave")]
    public class Wave : ScriptableObject
    {
        public List<Batch> batches = new();
        public float timeBetweenWaves;
    }
}