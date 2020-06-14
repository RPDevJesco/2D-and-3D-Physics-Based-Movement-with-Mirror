using UnityEngine;
using Mirror;

public class NetworkPlayerManager2D : NetworkBehaviour
{
    // Here we check for collision starting to occur and if it is with the wall or the floor but only on the server.
    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var grabValue = collision.transform.GetComponent<Movement2D>();

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            grabValue.IsGrounded = true;
        }
    }

    // We check to see if collisions are still occuring with either the floor or the wall, just the wall,
    // and just the floor.
    [ServerCallback]
    private void OnCollisionStay2D(Collision2D collision)
    {
        var grabValue = collision.transform.GetComponent<Movement2D>();

        if (collision.gameObject.CompareTag("Floor") && collision.gameObject.CompareTag("Wall"))
        {
            grabValue.IsGrounded = true;
        }

        if (!collision.gameObject.CompareTag("Floor") && collision.gameObject.CompareTag("Wall"))
        {
            grabValue.IsGrounded = true;
        }

        if (collision.gameObject.CompareTag("Floor") && !collision.gameObject.CompareTag("Wall"))
        {
            grabValue.IsGrounded = true;
        }
    }

    // If collision no longer occurs, then isgrounded is set to false.
    [ServerCallback]
    private void OnCollisionExit2D(Collision2D collision)
    {
        var grabValue = collision.transform.GetComponent<Movement2D>();

        grabValue.IsGrounded = false;
    }
}