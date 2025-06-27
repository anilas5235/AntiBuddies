using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    /// <summary>
    /// ScriptableObject containing configuration data for a weapon.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponSystem/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        /// <summary>
        /// The display name of the weapon.
        /// </summary>
        [SerializeField] private string weaponName;

        /// <summary>
        /// The type of the weapon.
        /// </summary>
        [SerializeField] private WeaponType weaponType;

        /// <summary>
        /// The tags associated with this weapon.
        /// </summary>
        [SerializeField] private List<WeaponTag> weaponTag;

        /// <summary>
        /// The prefab used to instantiate this weapon.
        /// </summary>
        [SerializeField] private GameObject prefab;

        /// <summary>
        /// Gets the prefab used to instantiate this weapon.
        /// </summary>
        public GameObject Prefab => prefab;

        /// <summary>
        /// Gets the display name of the weapon.
        /// </summary>
        public string WeaponName => weaponName;

        private enum WeaponTag
        {
            Explosive,
            Elemental,
            Primitive,
        }

        private enum WeaponType
        {
            Melee,
            Ranged,
        }
    }
}