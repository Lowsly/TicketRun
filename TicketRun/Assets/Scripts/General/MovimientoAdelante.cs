using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAdelante : MonoBehaviour
{
    public float speed = 0.2f;
    private Rigidbody2D rb;

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = transform.up * speed;
        rb.velocity = moveDirection;
    }
}
