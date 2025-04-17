using System;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        
        private IWeapon _currentWeapon;
        
        public bool HasWeapon => _currentWeapon != null;
        public bool IsEmpty => _currentWeapon == null;
        
        public void EquipWeapon(WeaponData newWeaponData)
        {
            ClearSlot();

            weaponData = newWeaponData;
            GameObject newWeapon = Instantiate(weaponData.Prefab, transform);
            newWeapon.transform.SetParent(transform);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            _currentWeapon = newWeapon.GetComponent<IWeapon>();
        }
        
        public void ClearSlot()
        {
            _currentWeapon?.DestroyWeapon();
            _currentWeapon = null;
            weaponData = null;
        }
    }
}