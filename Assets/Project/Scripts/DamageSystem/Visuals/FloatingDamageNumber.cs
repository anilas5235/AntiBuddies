using System;
using System.Collections;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    public class FloatingDamageNumber : MonoBehaviour
    {
        [SerializeField] private FloatingNumberData data;
        [SerializeField] private TextMesh textMesh;

        public void Setup(FloatingNumberData floatingNumberData){
            data = floatingNumberData;
            
            Vector3 pos = transform.position;
            pos.z = -1;
            transform.position = pos;
                
            UpdateText();
            StartCoroutine(LifeCycle());
        }

        public void Setup(EffectInfo effectInfo, float lifeTime)
        {
            Setup(new FloatingNumberData(effectInfo, lifeTime));
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.up * (Time.fixedDeltaTime * 2);
        }

        private void UpdateText()
        {
            textMesh.text = data.ToString();
            textMesh.color = data.GetColor();
        }

        private IEnumerator LifeCycle()
        {
            yield return new WaitForSeconds(data.lifeTime);
            Destroy(gameObject);
        }
    }
}