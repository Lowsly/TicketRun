using UnityEngine;

public class LineaRecta : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the shark forward in the direction it's currently facing using Rigidbody2D
        Vector2 moveDirection = transform.up * speed;
        rb.velocity = moveDirection;
    }
    
    // Adjust to set the shark's rotation based on the desired direction
    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
        
    }
}
