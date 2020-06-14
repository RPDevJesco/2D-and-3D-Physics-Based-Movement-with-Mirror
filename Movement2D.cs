using UnityEngine;
using MovementHandler;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : NetworkBehaviour
{
    // We can set the speed as we need it to be.
    public float Speed;
    // How high we want the object to jump
    public Vector2 jumpForce;
    // check to see if the object is in the air or on the ground
    public bool IsGrounded = false;
    // Horizontal Input.
    private float horizontal;
    // Jump input
    private float vertical;
    // Rigidbody component for physics
    [SerializeField] private Rigidbody2D rb;
    // Grab a reference to the movement backend
    private readonly MovementBackend _movementBackend = new MovementBackend();

    // Function that calls the backend's move function and fills in the data.
    private void Move()
    {
        _movementBackend.Move(rb,
            horizontal,
            0,
            Speed);
    }

    // Function that calls the backend's jump function and fills in the data.
    private void Jump()
    {
        if (IsGrounded != true) return;
        _movementBackend.Jump(rb, jumpForce, vertical);
    }

    // Physics update call. Only affects the Move and Jump
    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        Move();
        Jump();
    }

    // We use regular update to poll for input.
    private void Update()
    {
        if (!isLocalPlayer) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Jump");
    }
}
