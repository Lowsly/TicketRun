using UnityEngine;

public class MovimientoAdelante : MonoBehaviour
{
    public float speed = 0.2f;
    private Rigidbody2D rb;

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.up * speed;
    }
}
