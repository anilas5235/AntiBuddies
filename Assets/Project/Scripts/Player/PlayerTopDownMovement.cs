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

        // Input System Variables
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private Vector2 _moveInput;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();

            if (_playerInput)
            {
                _moveAction = _playerInput.actions["Move"];
            }
            else
            {
                Debug.LogError($"PlayerInput component not found on the GameObject {gameObject.name}.");
            }
        }

        private void OnEnable()
        {
            if (_moveAction != null)
            {
                _moveAction.performed += OnMovePerformed;
                _moveAction.canceled += OnMoveCanceled;
            }
        }

        private void OnDisable()
        {
            if (_moveAction != null)
            {
                _moveAction.performed -= OnMovePerformed;
                _moveAction.canceled -= OnMoveCanceled;
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _moveInput = Vector2.zero;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 targetVelocity = _moveInput.normalized * moveSpeed.CurrValue;
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