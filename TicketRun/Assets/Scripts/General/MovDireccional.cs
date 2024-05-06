using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovDireccional : MonoBehaviour
{
    public bool isXmoving;
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = new Vector2(isXmoving == true ? speed : 0, isXmoving == false ? speed : 0);
    }
}
