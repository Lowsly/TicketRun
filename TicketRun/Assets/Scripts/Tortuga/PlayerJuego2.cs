using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuego2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float upliftForce = 5.0f; 
    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, upliftForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            isDead = true;
            this.gameObject.layer = LayerMask.NameToLayer("Immune");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 1.5f;
            rb.AddForce(new Vector2(0, upliftForce*3), ForceMode2D.Impulse);
        }
    }
}