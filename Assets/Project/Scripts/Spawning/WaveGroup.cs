using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    [CreateAssetMenu(fileName = "WaveGroup", menuName = "SpawnSystem/WaveGroup")]
    public class WaveGroup : ScriptableObject
    {
        public List<Wave> waves = new();
    }
}