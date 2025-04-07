using Project.Scripts.BuffSystem.Data;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class BuffManager : MonoBehaviour
    {
        private DamageBuffHandler _damageBuffHandler;
        private HealBuffHandler _healBuffHandler;
        private StatBuffHandler _statBuffHandler;

        private void Awake()
        {
            _damageBuffHandler = new DamageBuffHandler(this);
            _healBuffHandler = new HealBuffHandler(this);
            _statBuffHandler = new StatBuffHandler(this);
        }

        public void AddBuff(DamageBuffData buff)
        {
        }
    }
}