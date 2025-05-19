using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponSystem/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private List<WeaponTag> weaponTag;
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
        public string WeaponName => weaponName;

        private enum WeaponTag
        {
            Explosive,
            Elemental,
        }
        
        private enum WeaponType
        {
            Melee,
            Ranged,
        }
    }
}