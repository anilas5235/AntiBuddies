using System.Collections;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumber : MonoBehaviour
    {
        private const float XOffsetRange = .7f;
        private const float YOffset = .5f;

        [SerializeField] private FloatingNumberData data;
        [SerializeField] private TextMesh textMesh;

        public void Setup(FloatingNumberData floatingNumberData)
        {
            data = floatingNumberData;

            Vector3 pos = transform.position;
            pos.z = -1;
            pos.x += Random.Range(-XOffsetRange, XOffsetRange);
            pos.y += YOffset;
            transform.position = pos;

            UpdateText();
            StartCoroutine(LifeCycle());
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