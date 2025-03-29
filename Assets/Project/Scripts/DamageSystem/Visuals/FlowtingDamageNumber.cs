using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    public class FloatingDamageNumber : MonoBehaviour
    {
        public int number;
        public DamageType damageType;
        [SerializeField] private bool living;


        private float timeRemaining;
        private float timeToLive = 1f;

        private void Awake()
        {
        }

        private void FixedUpdate()
        {
            timeRemaining -= Time.fixedDeltaTime;
            if (timeRemaining <= 0)
            {
                living = false;
            }
        }

        public void AddDamage(IDamage damage)
        {
            if (living)
            {
                if (damageType != damage.GetDamageType()) throw new Exception("Damage types do not match");
            }
            else
            {
                damageType = damage.GetDamageType();
                living = true;
            }

            number += damage.GetDamage();
            timeRemaining = timeToLive;
        }
    }
}