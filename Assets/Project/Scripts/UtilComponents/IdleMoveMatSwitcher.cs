using UnityEngine;

namespace Project.Scripts.Player
{
    public class IdleMoveMatSwitcher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rg2D;
        
        [SerializeField] private Material idleMat;
        [SerializeField] private Material moveMat;

        private bool _isIdle;
        
        protected bool IsIdle
        {
            get => _isIdle;
            set
            {
                if(_isIdle.Equals(value)) return;
                _isIdle = value;
                UpdateMaterial();
            }
        }

        private void FixedUpdate()
        {
            IsIdle = rg2D.linearVelocity.magnitude < 0.1f;
        }

        private void UpdateMaterial()
        {
            spriteRenderer.material = IsIdle ? idleMat : moveMat;
        }
    }
}