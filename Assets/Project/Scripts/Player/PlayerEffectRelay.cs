using System.Collections;
using System.Collections.Generic;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.ResourceSystem;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Player
{
    /// <summary>
    /// Relays effects on the player, handling damage application and temporary invulnerability ("eye frames").
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class PlayerEffectRelay : EffectRelay
    {
        /// <summary>
        /// List of stat packages applied on level up.
        /// /// </summary>
        [SerializeField] private List<StatPackage> levelUpStatPackages;

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

        private void OnEnable()
        {
            ExpManager.Instance.OnLevelUp += OnLevelUp;
            GlobalVariables.Instance.OnWaveStart += OnWaveStart;
        }

        private void OnDisable()
        {
            ExpManager.Instance.OnLevelUp -= OnLevelUp;
            GlobalVariables.Instance.OnWaveStart -= OnWaveStart;
        }

        /// <summary>
        /// Handles level up events by applying all stat packages defined for level up.
        /// </summary>
        private void OnLevelUp()
        {
            // Applies all stat packages defined for level up.
            foreach (StatPackage package in levelUpStatPackages)
            {
                Apply(package);
            }
        }
        
        /// <summary>
        /// Handles the start of a new wave by fully healing the player.
        /// </summary>
        private void OnWaveStart()
        {
            healthComponent.FullHeal();
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