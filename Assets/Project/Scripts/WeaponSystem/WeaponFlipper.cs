using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class WeaponFlipper : MonoBehaviour
    {
        private float _offsetAngle;
        private bool _isFlipped;

        private void Awake()
        {
            _offsetAngle = transform.localRotation.eulerAngles.z;
        }

        private void FixedUpdate()
        {
            float parentAngle = Mathf.Abs(transform.parent.rotation.eulerAngles.z);
            bool flip = parentAngle is > 90 and < 270;
            if (flip == _isFlipped) return;

            _isFlipped = flip;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            transform.localRotation =
                Quaternion.Euler(0f, 0f, (_isFlipped ? 180f : 0f) + _offsetAngle * (_isFlipped ? -1 : 1));
        }
    }
}