using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class EffectType : ScriptableObject
    {
        [SerializeField] private string typeName = "no name yet";
        [SerializeField] private string description = "no description jet";
        

        public string Name => typeName;
        public string Description => description;
    }
}