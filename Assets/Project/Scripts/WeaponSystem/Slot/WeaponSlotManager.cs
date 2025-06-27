using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Slot
{
    /// <summary>
    /// Manages a collection of weapon slots, their positions, and weapon assignment.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class WeaponSlotManager : MonoBehaviour
    {
        /// <summary>
        /// The base radius of the circle on which weapon slots are positioned.
        /// </summary>
        [SerializeField, Range(0, 10)] private float slotCircleRadius = 1f;

        /// <summary>
        /// The maximum number of weapon slots allowed.
        /// </summary>
        [SerializeField, Range(0, 20)] private int numberOfSlots = 6;

        /// <summary>
        /// The current number of weapon slots created.
        /// </summary>
        [SerializeField] private int currentNumberOfSlots;

        /// <summary>
        /// The prefab used to instantiate new weapon slots.
        /// </summary>
        [SerializeField] private GameObject weaponSlotPrefab;

        /// <summary>
        /// The default weapons to assign to slots at startup.
        /// </summary>
        [SerializeField] private WeaponData[] defaultWeaponData;

        /// <summary>
        /// The list of all weapon slots managed by this manager.
        /// </summary>
        private readonly List<WeaponSlot> _weaponSlots = new();

        /// <summary>
        /// Extra buffering space added to the radius for each additional slot.
        /// </summary>
        private const float ExtraBufferingPerSlot = 0.05f;

        private void Start()
        {
            foreach (WeaponData data in defaultWeaponData)
            {
                GetEmptySlot()?.EquipWeapon(data);
            }
        }

        /// <summary>
        /// Finds an empty weapon slot or adds a new one if possible.
        /// </summary>
        /// <returns>An empty WeaponSlot, or null if none available and cannot add more.</returns>
        private WeaponSlot GetEmptySlot()
        {
            WeaponSlot slot = _weaponSlots.FirstOrDefault(slot => slot.IsEmpty);
            slot ??= AddWeaponSlot();
            return slot;
        }

        /// <summary>
        /// Adds a new weapon slot if the maximum has not been reached.
        /// </summary>
        /// <returns>The newly added WeaponSlot, or null if not added.</returns>
        private WeaponSlot AddWeaponSlot()
        {
            if (currentNumberOfSlots >= numberOfSlots) return null;
            currentNumberOfSlots++;
            BuildWeaponSlots();
            return _weaponSlots.Last();
        }

        /// <summary>
        /// Builds or updates the weapon slots and positions them in a circle.
        /// </summary>
        private void BuildWeaponSlots()
        {
            List<Vector3> positions = CalculateSlotPositions(currentNumberOfSlots);
            for (int i = 0; i < positions.Count; i++)
            {
                Vector3 position = positions[i];
                // Instantiate or reuse the weapon slot object
                WeaponSlot weaponSlot = i >= _weaponSlots.Count ? CreateWeaponSlot(position) : _weaponSlots[i];
                weaponSlot.transform.localPosition = position;
            }

            // Removes any excess weapon slots if the current number exceeds the defined limit
            for (int i = _weaponSlots.Count - 1; i >= numberOfSlots; i--)
            {
                Destroy(_weaponSlots[i].gameObject);
                _weaponSlots.RemoveAt(i);
            }
        }

        /// <summary>
        /// Instantiates a new weapon slot at the specified position.
        /// </summary>
        /// <param name="position">The local position for the new slot.</param>
        /// <returns>The created WeaponSlot instance.</returns>
        private WeaponSlot CreateWeaponSlot(Vector3 position)
        {
            GameObject weaponSlotObject = Instantiate(weaponSlotPrefab, position, Quaternion.identity, transform);
            WeaponSlot weaponSlot = weaponSlotObject.GetComponent<WeaponSlot>();
            _weaponSlots.Add(weaponSlot);
            return weaponSlot;
        }

        /// <summary>
        /// Calculates the positions for the given number of weapon slots in a circular pattern.
        /// </summary>
        /// <param name="numOfSlots">The number of slots to position.</param>
        /// <returns>A list of local positions for each slot.</returns>
        private List<Vector3> CalculateSlotPositions(int numOfSlots)
        {
            List<Vector3> positions = new();
            float deltaAngle = 2 * Mathf.PI / numOfSlots;
            for (int i = 0; i < numOfSlots; i++)
            {
                float angle = i * deltaAngle;
                // Adjust radius based on the number of slots
                float radius = slotCircleRadius + numOfSlots * ExtraBufferingPerSlot;
                // Calculate the position of the weapon slot in a circular pattern
                Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                positions.Add(position);
            }

            return positions;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Draw the circle representing the slot positions in the editor
            const float radius = 0.3f;
            Vector3 scaledUp = transform.up * radius;
            Vector3 scaledRight = transform.right * radius;
            Gizmos.color = Color.yellow;
            foreach (Vector3 position in CalculateSlotPositions(numberOfSlots))
            {
                Vector3 pos = position + transform.position; // Adjust position to be relative to the manager's position
                Gizmos.DrawWireSphere(pos, radius);
                Gizmos.DrawLine(pos - scaledUp, pos + scaledUp);
                Gizmos.DrawLine(pos - scaledRight, pos + scaledRight);
            }
        }
#endif
    }
}