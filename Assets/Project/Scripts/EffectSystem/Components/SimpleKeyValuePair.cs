using System;

namespace Project.Scripts.EffectSystem.Components
{
    [Serializable]
    internal struct SimpleKeyValuePair<T1, T2>
    {
        public T1 Key;
        public T2 Value;

        public SimpleKeyValuePair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }
    }
}