using System.Collections;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using UnityEngine;

namespace Project.Scripts.Player
{
    /// <summary>
    /// Relays effects on the player, handling damage application and temporary invulnerability ("eye frames").
    /// </summary>
    public class PlayerEffectRelay : EffectRelay
    {
        /// <summary>
        /// Number of fixed update frames for temporary invulnerability after taking damage.
        /// </summary>
        private const float EyeFrames = 10;
        /// <summary>
        /// Coroutine reference for managing eye frames.
        /// </summary>
        private Coroutine _eyeFrameCoroutine;

        /// <summary>
        /// Initializes the player effect relay and sets the allied group to Player.
        /// </summary>
        public PlayerEffectRelay()
        {
            alliedGroup = AlliedGroup.Player;
        }

        /// <inheritdoc/>
        public override void Apply(DamagePackage package)
        {
            // Prevents applying damage if currently in eye frames (invulnerable).
            if (_eyeFrameCoroutine != null) return;
            base.Apply(package);
            StartEyeFrame();
        }

        /// <inheritdoc/>
        protected override Color GetDamageColor(DamagePackage damagePackage)
        {
            // Returns red color for damage, indicating the player has taken damage.
            return Color.red;
        }

        /// <summary>
        /// Starts the eye frame coroutine to provide temporary invulnerability.
        /// </summary>
        private void StartEyeFrame()
        {
            _eyeFrameCoroutine = StartCoroutine(EyeFrameCoroutine());
        }

        /// <summary>
        /// Coroutine that manages the duration of eye frames (temporary invulnerability).
        /// </summary>
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