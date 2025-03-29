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
        
        private DamageReceiver damageReceiver;

        private void Awake()
        {
            damageReceiver = GetComponent<DamageReceiver>();
        }

        private void OnEnable()
        {
            damageReceiver.OnDamageReceived += HandleDamageReceived;
        }

        private void OnDisable()
        {
            damageReceiver.OnDamageReceived -= HandleDamageReceived;
        }

        private void HandleDamageReceived(DamageEvent damageEvent)
        {
            if (damageEvent.DamageAmount <= 0 || damageNumberPrefab == null) return;
            
            SpawnDamageNumber(damageEvent.DamageAmount, damageEvent.Position);
        }

        private void SpawnDamageNumber(int damageAmount, Vector3 position)
        {
            GameObject damageNumberInstance = Instantiate(damageNumberPrefab, position + offset, Quaternion.identity);
            TextMeshPro textMesh = damageNumberInstance.GetComponent<TextMeshPro>();
            
            if (textMesh != null)
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
