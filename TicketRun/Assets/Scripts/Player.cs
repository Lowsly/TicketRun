using UnityEngine;

public class Player : MonoBehaviour
{
    public float upwardForce = 10f;
    public float gravityWhilePressed = 0.5f;
    public float defaultGravity = 1f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = defaultGravity; // Set default gravity scale
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.gravityScale = gravityWhilePressed; // Set lower gravity while the mouse is pressed
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
        }
        else
        {
            rb.gravityScale = defaultGravity; // Reset to default gravity when the mouse is not pressed
        }
    }
}
