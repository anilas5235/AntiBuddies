using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    /// <summary>
    /// Flips the weapon's sprite and orientation based on the parent's rotation.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class WeaponFlipper : MonoBehaviour
    {
        /// <summary>
        /// The initial local rotation angle offset.
        /// </summary>
        private float _offsetAngle;

        /// <summary>
        /// Whether the weapon is currently flipped.
        /// </summary>
        private bool _isFlipped;

        private void Awake()
        {
            _offsetAngle = transform.localRotation.eulerAngles.z;
        }

        private void FixedUpdate()
        {
            // Determine if the parent rotation requires flipping the weapon
            float parentAngle = Mathf.Abs(transform.parent.rotation.eulerAngles.z);
            bool flip = parentAngle is > 90 and < 270;
            if (flip == _isFlipped) return;

            _isFlipped = flip;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            // Adjust the local rotation to match the flipped state
            transform.localRotation =
                Quaternion.Euler(0f, 0f, (_isFlipped ? 180f : 0f) + _offsetAngle * (_isFlipped ? -1 : 1));
        }
    }
}