using System.Collections;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumber : MonoBehaviour
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