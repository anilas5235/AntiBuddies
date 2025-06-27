using System.Collections;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class RegenComponent : MonoBehaviour, INeedStatGroup
    {
        [SerializeField] private StatRef regenStat;
        [SerializeField] private EffectRelay target;
        [SerializeField] private HealPackage healPackage;

        private Coroutine _regenCoroutine;
        private float _regenInterval;

        private void OnEnable()
        {
            regenStat.Stat.OnStatChange += UpdateRegenInterval;
            UpdateRegenInterval();
        }

        private void OnDisable()
        {
            regenStat.Stat.OnStatChange -= UpdateRegenInterval;
            StopRegen();
        }

        private void StartRegen()
        {
            if (_regenCoroutine != null) return;
            _regenCoroutine = StartCoroutine(RegenCoroutine());
        }

        private void StopRegen()
        {
            if (_regenCoroutine == null) return;
            StopCoroutine(_regenCoroutine);
            _regenCoroutine = null;
        }

        private IEnumerator RegenCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_regenInterval);
                target.Apply(healPackage);
            }
        }

        private void UpdateRegenInterval()
        {
            float regen = regenStat.GetValue();
            if (regen <= 0)
            {
                StopRegen();
                return;
            }

            _regenInterval = 1 / (0.05f * regen);
            StartRegen();
        }

        public void OnStatInit(IStatGroup statGroup)
        {
            regenStat.OnStatInit(statGroup);
        }
    }
}