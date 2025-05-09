using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public class DataCollection<T>
    {
        [SerializeField] private List<T> data = new();

        protected List<T> Data => data;
        
        public int Count => data.Count;
        public bool IsEmpty => data.Count == 0;

        public virtual void Add(T t)
        {
            data.Add(t);
        }

        public void Clear()
        {
            data.Clear();
        }
    }
}