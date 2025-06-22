using System.Collections;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerEffectRelay : EffectRelay
    {
        private const float EyeFrames = 10;
        private Coroutine _eyeFrameCoroutine;

        public PlayerEffectRelay()
        {
            alliedGroup = AlliedGroup.Player;
        }

        public override void Apply(DamagePackage package)
        {
            if (_eyeFrameCoroutine != null) return;
            base.Apply(package);
            StartEyeFrame();
        }

        protected override Color GetDamageColor(DamagePackage damagePackage)
        {
            return Color.red;
        }

        private void StartEyeFrame()
        {
            _eyeFrameCoroutine = StartCoroutine(EyeFrameCoroutine());
        }

        private IEnumerator EyeFrameCoroutine()
        {
            for (int i = 0; i < EyeFrames; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            _eyeFrameCoroutine = null;
        }
    }
}