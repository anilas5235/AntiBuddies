using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class TempSpriteEffect : LiveTimePoolableMono
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        protected override void LiveTick()
        {
            if(timeToLive > 1f) return;
            SetSpriteAlpha(timeToLive);
        }

        public override void Reset()
        {
            base.Reset();
            SetSpriteAlpha(1f);
        }

        private void SetSpriteAlpha(float alpha)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}