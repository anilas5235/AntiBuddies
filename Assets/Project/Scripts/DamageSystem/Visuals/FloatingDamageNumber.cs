using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    public class FloatingDamageNumber : MonoBehaviour
    {
        public DamageInfo damageInfo;
        [SerializeField] private bool living = true;
        [SerializeField] private float TimeToLive = 1f;


        private float _timeRemaining;
        private TextMesh _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMesh>();
        }

        private void Start()
        {
            UpdateText();
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
            _timeRemaining = TimeToLive;
            UpdateText();
        }

        public void UpdateText()
        {
            _textMesh.text = damageInfo.damage.ToString();
            _textMesh.color = damageInfo.damageType.GetColor();
        }
    }
}