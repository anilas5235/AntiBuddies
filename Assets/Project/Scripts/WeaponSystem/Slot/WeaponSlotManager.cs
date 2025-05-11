using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Slot
{
    public class WeaponSlotManager : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float slotCircleRadius = 1f;
        [SerializeField, Range(0, 20)] private int numberOfSlots = 6;
        [SerializeField] private int currentNumberOfSlots;
        [SerializeField] private GameObject weaponSlotPrefab;
        [SerializeField] private WeaponData[] defaultWeaponData;
        private readonly List<WeaponSlot> _weaponSlots = new();

        private const float ExtraBufferingPerSlot = 0.05f;

        private void Start()
        {
            foreach (WeaponData data in defaultWeaponData)
            {
                GetEmptySlot()?.EquipWeapon(data);
            }
        }

        private WeaponSlot GetEmptySlot()
        {
            WeaponSlot slot = _weaponSlots.FirstOrDefault(slot => slot.IsEmpty);
            slot ??= AddWeaponSlot();
            return slot;
        }

        private WeaponSlot AddWeaponSlot()
        {
            if (currentNumberOfSlots >= numberOfSlots) return null;
            currentNumberOfSlots++;
            BuildWeaponSlots();
            return _weaponSlots.Last();
        }

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

            // Remove any unused weapon slots
            for (int i = _weaponSlots.Count - 1; i >= numberOfSlots; i--)
            {
                Destroy(_weaponSlots[i].gameObject);
                _weaponSlots.RemoveAt(i);
            }
        }

        private WeaponSlot CreateWeaponSlot(Vector3 position)
        {
            GameObject weaponSlotObject = Instantiate(weaponSlotPrefab, position, Quaternion.identity, transform);
            WeaponSlot weaponSlot = weaponSlotObject.GetComponent<WeaponSlot>();
            _weaponSlots.Add(weaponSlot);
            return weaponSlot;
        }

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
            const float radius = 0.3f;
            Vector3 scaledUp = transform.up * radius;
            Vector3 scaledRight = transform.right * radius;
            Gizmos.color = Color.yellow;
            foreach (Vector3 position in CalculateSlotPositions(numberOfSlots))
            {
                Gizmos.DrawWireSphere(position, radius);
                Gizmos.DrawLine(position - scaledUp, position + scaledUp);
                Gizmos.DrawLine(position - scaledRight, position + scaledRight);
            }
        }
#endif
    }
}