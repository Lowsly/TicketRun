using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	public float jumpForce = 2.5f;

	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;


	private Rigidbody2D _rigidbody;

	private bool _isGrounded;


	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		
	}

    public void Update()
    {	
			_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
			
			if (Input.GetMouseButtonDown(0) && _isGrounded == true) {
				_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			}
		
	}
	
	
}

