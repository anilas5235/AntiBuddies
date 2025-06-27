using UnityEngine;

namespace Project.Scripts.WeaponSystem.Slot
{
    /// <summary>
    /// Represents a slot that can hold and manage a weapon instance.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class WeaponSlot : MonoBehaviour
    {
        /// <summary>
        /// The data for the weapon currently equipped in this slot.
        /// </summary>
        [SerializeField] private WeaponData weaponData;

        private IWeapon _currentWeapon;

        /// <summary>
        /// Gets whether this slot currently has a weapon equipped.
        /// </summary>
        public bool HasWeapon => !IsEmpty;

        /// <summary>
        /// Gets whether this slot is empty (no weapon equipped).
        /// </summary>
        public bool IsEmpty => _currentWeapon == null;

        /// <summary>
        /// Equips a new weapon in this slot, replacing any existing weapon.
        /// </summary>
        /// <param name="newWeaponData">The data for the weapon to equip.</param>
        public void EquipWeapon(WeaponData newWeaponData)
        {
            ClearSlot();

            weaponData = newWeaponData;
            GameObject newWeapon = Instantiate(weaponData.Prefab, transform);
            newWeapon.transform.localPosition = Vector3.zero;
            _currentWeapon = newWeapon.GetComponent<IWeapon>();
        }

        /// <summary>
        /// Removes the current weapon from the slot and clears its data.
        /// </summary>
        public void ClearSlot()
        {
            _currentWeapon?.DestroyWeapon();
            _currentWeapon = null;
            weaponData = null;
        }

        /// <summary>
        /// Gets the default angle for the weapon based on the slot's local position.
        /// </summary>
        /// <returns>0 if on the right, 180 if on the left.</returns>
        public float GetDefaultWeaponAngle()
        {
            return transform.localPosition.x > 0 ? 0f : 180f;
        }
    }
}