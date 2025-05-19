using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    public abstract class ItemBehaviour : ScriptableObject
    {
        public abstract void OnAdded(EffectRelay effectRelay);
        public abstract void OnRemoved(EffectRelay effectRelay);
    }
}