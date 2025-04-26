using UnityEngine;

namespace Project.Scripts.Utils
{
    /// <summary>
    ///   <para>class driving form this class will act as Singletons</para>
    /// </summary>

    public abstract class Singleton<T> : MonoBehaviour where T :MonoBehaviour
    {
        public static Singleton<T> Instance { get; private set; }

        protected virtual void Awake()
        {
            if (!Instance) Instance = this;
            else if(Instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(this);
            }
        }
    }
}