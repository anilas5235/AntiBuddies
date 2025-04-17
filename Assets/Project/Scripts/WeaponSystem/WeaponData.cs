using System;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeField] private GameObject prefab;
        
        public GameObject Prefab => prefab;
    }
}