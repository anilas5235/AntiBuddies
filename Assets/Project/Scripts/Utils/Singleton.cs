using UnityEngine;

namespace Project.Scripts.Utils
{
    /// <summary>
    ///   <para>class driving form this class will act as Singletons</para>
    /// </summary>

    [DefaultExecutionOrder(-100)]
    public abstract class Singleton<T> : MonoBehaviour where T :MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (!Instance) Instance = gameObject.GetComponent<T>();
            else if(Instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(this);
            }
        }
    }
}