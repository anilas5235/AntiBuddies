using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerTopDownMovement : MonoBehaviour, INeedStatComponent
    {
        [Range(0.0001f, 50f)] public float maxAcceleration = 2f;

        [SerializeField] private ValueStatRef moveSpeed;

        private Rigidbody2D _rb2d;
        private Vector2 _moveInput;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        public void OnMove(InputValue inputValue)
        {
            _moveInput = inputValue.Get<Vector2>().normalized;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 targetVelocity = _moveInput * moveSpeed.CurrValue;
            _rb2d.linearVelocity =
                Vector2.MoveTowards(_rb2d.linearVelocity, targetVelocity,
                    Time.fixedDeltaTime * maxAcceleration * moveSpeed.CurrValue);
        }

        public void OnStatInit(StatComponent statComponent)
        {
            moveSpeed.Init(statComponent);
        }

        private void OnValidate()
        {
            moveSpeed.UpdateValue();
        }
    }
}