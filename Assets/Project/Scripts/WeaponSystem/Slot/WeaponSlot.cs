using UnityEngine;

namespace Project.Scripts.WeaponSystem.Slot
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        
        private IWeapon _currentWeapon;
        private WeaponSlotManager _weaponSlotManager;
        public void SetWeaponSlotManager(WeaponSlotManager weaponSlotManager) => _weaponSlotManager = weaponSlotManager;

        public bool HasWeapon => _currentWeapon != null;
        public bool IsEmpty => _currentWeapon == null;
        
        public void EquipWeapon(WeaponData newWeaponData)
        {
            ClearSlot();

            weaponData = newWeaponData;
            GameObject newWeapon = Instantiate(weaponData.Prefab, transform);
            newWeapon.transform.localPosition = Vector3.zero;
            _currentWeapon = newWeapon.GetComponent<IWeapon>();
            _currentWeapon.SetWeaponSlot(this);
        }
        
        public void ClearSlot()
        {
            _currentWeapon?.DestroyWeapon();
            _currentWeapon = null;
            weaponData = null;
        }
    }
}