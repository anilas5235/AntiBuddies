using UnityEngine;

namespace Project.Scripts.WeaponSystem.Slot
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;

        private IWeapon _currentWeapon;
        public bool HasWeapon => !IsEmpty;
        public bool IsEmpty => _currentWeapon == null;

        public void EquipWeapon(WeaponData newWeaponData)
        {
            ClearSlot();

            weaponData = newWeaponData;
            GameObject newWeapon = Instantiate(weaponData.Prefab, transform);
            newWeapon.transform.localPosition = Vector3.zero;
            _currentWeapon = newWeapon.GetComponent<IWeapon>();
        }

        public void ClearSlot()
        {
            _currentWeapon?.DestroyWeapon();
            _currentWeapon = null;
            weaponData = null;
        }

        public float GetDefaultWeaponAngle()
        {
            return transform.localPosition.x > 0 ? 0f : 180f;
        }
    }
}