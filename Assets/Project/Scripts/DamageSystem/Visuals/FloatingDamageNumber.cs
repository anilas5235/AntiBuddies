using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    public class FloatingDamageNumber : MonoBehaviour
    {
        public DamageInfo damageInfo;
        [SerializeField] private bool living;


        private float _timeRemaining;
        private const float TimeToLive = 1f;
        private TextMesh _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMesh>();
        }

        private void FixedUpdate()
        {
            _timeRemaining -= Time.fixedDeltaTime;
            if (_timeRemaining <= 0)
            {
                living = false;
            }
        }

        public void AddDamage(DamageInfo damage)
        {
            if (living)
            {
                if (damage.damageType != damage.GetDamageType()) throw new Exception("Damage types do not match");
            }
            else
            {
                damage.damageType = damage.GetDamageType();
                living = true;
            }

            damage.damage += damage.GetDamage();
            _textMesh.text = damageInfo.damage.ToString();
            _timeRemaining = TimeToLive;
        }
    }
}