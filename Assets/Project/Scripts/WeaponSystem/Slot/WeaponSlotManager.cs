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
        [SerializeField] private GameObject weaponSlotPrefab;
        private readonly List<WeaponSlot> _weaponSlots = new();

        private const float ExtraBufferingPerSlot = 0.05f;

        private void Awake()
        {
            BuildWeaponSlots();
        }
        
        public WeaponSlot GetEmptySlot()
        {
            return _weaponSlots.FirstOrDefault(slot => slot.IsEmpty);
        }
        
        public WeaponSlot AddWeaponSlot(){
            numberOfSlots++;
            BuildWeaponSlots();
            return _weaponSlots.Last();
        }

        private void BuildWeaponSlots()
        {
            List<Vector3> positions = CalculateSlotPositions();
            for (int i = 0; i < positions.Count; i++)
            {
                Vector3 position = positions[i];
                // Instantiate or reuse the weapon slot object
                GameObject weaponSlotObject = i >= _weaponSlots.Count
                    ? Instantiate(weaponSlotPrefab, transform)
                    : _weaponSlots[i].gameObject;

                weaponSlotObject.transform.localPosition = position;
                weaponSlotObject.transform.localRotation = Quaternion.identity;

                WeaponSlot weaponSlot = weaponSlotObject.GetComponent<WeaponSlot>();
                if (!weaponSlot)
                {
                    throw new ArgumentException($"WeaponSlot component not found on {weaponSlotObject.name}");
                }
                _weaponSlots.Add(weaponSlot);
            }
            
            // Remove any unused weapon slots
            for (int i = _weaponSlots.Count - 1; i >= numberOfSlots; i--)
            {
                Destroy(_weaponSlots[i].gameObject);
                _weaponSlots.RemoveAt(i);
            }
        }

        private List<Vector3> CalculateSlotPositions()
        {
            List<Vector3> positions = new();
            float deltaAngle = 2 * Mathf.PI / numberOfSlots;
            for (int i = 0; i < numberOfSlots; i++)
            {
                float angle = i * deltaAngle;
                // Adjust radius based on the number of slots
                float radius = slotCircleRadius + numberOfSlots * ExtraBufferingPerSlot;
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
            foreach (Vector3 position in CalculateSlotPositions())
            {
                Gizmos.DrawWireSphere(position, radius);
                Gizmos.DrawLine(position - scaledUp, position + scaledUp);
                Gizmos.DrawLine(position - scaledRight, position + scaledRight);
            }
        }
#endif
    }
}