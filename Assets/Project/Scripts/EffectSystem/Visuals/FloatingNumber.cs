using System.Collections;
using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumber : LiveTimePoolableMono
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
            lifeTime = floatingNumberData.lifeTime;
            
            UpdateText();
        }

        private void UpdateText()
        {
            textMesh.text = data.ToString();
            textMesh.color = data.GetColor();
        }

        public override void Reset()
        {
            base.Reset();
            textMesh.color = Color.white;
        }
    }
}