using UnityEngine;

namespace Project.Scripts.UtilComponents
{
    /// <summary>
    /// Switches the material of a SpriteRenderer based on whether the attached Rigidbody2D is idle or moving.
    /// </summary>
    public class IdleMoveMatSwitcher : MonoBehaviour
    {
        /// <summary>
        /// The SpriteRenderer whose material will be switched.
        /// </summary>
        [SerializeField] private SpriteRenderer spriteRenderer;

        /// <summary>
        /// The Rigidbody2D used to determine movement state.
        /// </summary>
        [SerializeField] private Rigidbody2D rg2D;

        /// <summary>
        /// Material to use when idle.
        /// </summary>
        [SerializeField] private Material idleMat;

        /// <summary>
        /// Material to use when moving.
        /// </summary>
        [SerializeField] private Material moveMat;

        private bool _isIdle;

        /// <summary>
        /// True if the object is considered idle (velocity below threshold).
        /// </summary>
        private bool IsIdle
        {
            get => _isIdle;
            set
            {
                if (_isIdle.Equals(value)) return;
                _isIdle = value;
                UpdateMaterial();
            }
        }

        private void FixedUpdate()
        {
            // Consider idle if velocity is very low.
            IsIdle = rg2D.linearVelocity.magnitude < 0.1f;
        }

        /// <summary>
        /// Updates the SpriteRenderer's material based on idle/move state.
        /// </summary>
        private void UpdateMaterial()
        {
            spriteRenderer.material = IsIdle ? idleMat : moveMat;
        }
    }
}