using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDownMovement : MonoBehaviour
{
    [Range(0.1f, 60)] public float speed = 4f;
    [Range(0.0001f, 2)] public float maxAcceleration = 1;


    private Rigidbody2D rb2d;

    // Input System Variablen
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 moveInput;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput)
        {
            moveAction = playerInput.actions["Move"];
        }
        else
        {
            Debug.LogError($"PlayerInput component not found on the GameObject {gameObject.name}.");
        }
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.performed += OnMovePerformed;
            moveAction.canceled += OnMoveCanceled;
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMovePerformed;
            moveAction.canceled -= OnMoveCanceled;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 targetVelocity = moveInput * speed;

        rb2d.linearVelocity =
            Vector2.MoveTowards(rb2d.linearVelocity, targetVelocity, Time.fixedDeltaTime * 10 * maxAcceleration);
    }
}