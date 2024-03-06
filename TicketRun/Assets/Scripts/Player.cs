using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.IsTouchingLayers(capsuleCollider, groundLayer);

        // Jump when the Space key is pressed and the player is grounded
        if (Input.GetMouseButton(0) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
