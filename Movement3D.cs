using UnityEngine;
using MovementHandler;
using Mirror;

// Force this script to only work if a Rigidbody is attached to the object this script is attached to.
[RequireComponent(typeof(Rigidbody))]
public class Movement3D : NetworkBehaviour
{
    // We can set the speed as we need it to be.
    public float Speed;
    // How high we want the object to jump
    private float jumpForce = 10f;
    // check to see if the object is in the air or on the ground
    public bool IsGrounded = false;
    // Rigidbody component for physics
    [SerializeField] private Rigidbody rb;
    // Horizontal Input value
    private float horizontal;
    // Vertical Input value
    private float vertical;
    // Jump Input
    private float jumpInput;
    // Grab a reference to the movement backend
    private readonly MovementBackend _movementBackend = new MovementBackend();

    // Function that calls the backend's move function and fills in the data.
    void Move()
    {
        _movementBackend.Move(this.transform, rb,
            horizontal,
            0,
            vertical,
            Speed,
            Time.fixedDeltaTime);
    }

    // Function that calls the backend's jump function and fills in the data.
    void Jump()
    {
        _movementBackend.Jump(rb, IsGrounded, jumpInput, jumpForce);
    }

    // Physics update call. Only affects the Move and Jump
    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        Move();
        Jump();
    }

    // Polls the inputs from Update to pass to our horizontal and vertical input calls in physics update.
    private void Update()
    {
        if (!isLocalPlayer) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
    }

}
