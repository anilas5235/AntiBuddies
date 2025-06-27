using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    /// <summary>
    /// ScriptableObject containing configuration data for a projectile.
    /// </summary>
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "WeaponSystem/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        /// <summary>
        /// The prefab to instantiate for this projectile.
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// The sprite to use for the projectile's appearance.
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// The scale to apply to the projectile.
        /// </summary>
        public Vector3 scale = Vector3.one;

        /// <summary>
        /// The speed at which the projectile travels.
        /// </summary>
        public float speed;

        /// <summary>
        /// The base number of contacts the projectile can make before being destroyed.
        /// Additional contacts may be added at runtime.
        /// </summary>
        public int contacts = 1;
    }
}