using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Player
{
    /// <summary>
    /// Handles top-down movement for the player using Rigidbody2D and input system.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerTopDownMovement : MonoBehaviour, INeedStatGroup
    {
        /// <summary>
        /// Maximum acceleration applied to the player movement.
        /// </summary>
        [Range(0.0001f, 50f)] public float maxAcceleration = 2f;

        /// <summary>
        /// Reference to the movement speed stat.
        /// </summary>
        [SerializeField] private ValueStatRef moveSpeed;

        /// <summary>
        /// Cached reference to the Rigidbody2D component.
        /// </summary>
        private Rigidbody2D _rb2d;

        /// <summary>
        /// Stores the current movement input direction.
        /// </summary>
        private Vector2 _moveInput;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Called by the input system to update movement input.
        /// </summary>
        /// <param name="inputValue">The input value representing movement direction.</param>
        public void OnMove(InputValue inputValue)
        {
            _moveInput = inputValue.Get<Vector2>().normalized;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        /// <summary>
        /// Applies movement to the Rigidbody2D based on input and stats.
        /// </summary>
        private void HandleMovement()
        {
            float currentStatSpeed = moveSpeed.CurrValue;
            Vector2 targetVelocity = _moveInput * currentStatSpeed;

            // Calculate the maximum change in velocity allowed this frame (acceleration constraint).
            float maxDistanceDelta = Time.fixedDeltaTime * maxAcceleration * currentStatSpeed;

            // Smoothly move the Rigidbody2D's velocity towards the target velocity.
            _rb2d.linearVelocity = Vector2.MoveTowards(_rb2d.linearVelocity, targetVelocity, maxDistanceDelta);
        }

        /// <inheritdoc/>
        public void OnStatInit(IStatGroup statGroup)
        {
            // Initialize the move speed reference from the stat component.
            moveSpeed.OnStatInit(statGroup);
        }

        private void OnValidate()
        {
            // Updates the move speed value in the editor when values change.
            moveSpeed.UpdateValue();
        }
    }
}