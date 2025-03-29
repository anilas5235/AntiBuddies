using System.Collections;
using Project.Scripts.DamageSystem.Components;
using Project.Scripts.DamageSystem.Events;
using TMPro;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    public class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject damageNumberPrefab;
        [SerializeField] private float displayDuration = 1.0f;
        [SerializeField] private Vector3 offset = new Vector3(0, 0.5f, 0);
        [SerializeField] private float floatSpeed = 1.0f;
        
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            _healthComponent.OnDamageReceived += HandleDamageReceived;
        }

        private void OnDisable()
        {
            _healthComponent.OnDamageReceived -= HandleDamageReceived;
        }

        private void HandleDamageReceived(DamageEvent damageEvent)
        {
            if (damageEvent.DamageAmount <= 0 || !damageNumberPrefab) return;
            
            SpawnDamageNumber(damageEvent.DamageAmount);
        }

        private void SpawnDamageNumber(int damageAmount)
        {
            GameObject damageNumberInstance = Instantiate(damageNumberPrefab, offset, Quaternion.identity);
            TextMeshPro textMesh = damageNumberInstance.GetComponent<TextMeshPro>();
            
            if (textMesh)
            {
                textMesh.text = damageAmount.ToString();
                StartCoroutine(AnimateDamageNumber(damageNumberInstance));
            }
            else
            {
                Destroy(damageNumberInstance);
            }
        }

        private IEnumerator AnimateDamageNumber(GameObject damageNumber)
        {
            float elapsed = 0f;
            Vector3 startPosition = damageNumber.transform.position;
            
            while (elapsed < displayDuration)
            {
                damageNumber.transform.position = startPosition + new Vector3(0, floatSpeed * elapsed, 0);
                
                // Optional: Fade out towards the end
                TextMeshPro textMesh = damageNumber.GetComponent<TextMeshPro>();
                if (textMesh != null && elapsed > displayDuration * 0.5f)
                {
                    Color color = textMesh.color;
                    color.a = 1 - ((elapsed - displayDuration * 0.5f) / (displayDuration * 0.5f));
                    textMesh.color = color;
                }
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            Destroy(damageNumber);
        }
    }
}
