using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class LineaRecta : MonoBehaviour
{
    public float speed = 1.0f;
    public float difficulty;
    private Rigidbody2D rb;
    private bool shouldChangeDirection = false;
    private float timeUntilChange = 0;
    private bool updateActivated = true;
    private float randomTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Randomly decide whether to change direction based on difficulty
        int randomDecision = Random.Range(10, Mathf.FloorToInt(difficulty) * 10 + 1); // +1 to make the upper bound inclusive
        if (randomDecision > 0)
        {
            shouldChangeDirection = true;
            randomTime = Random.Range(1f, 6f);
        }
    }

    void Update()
    {
        if (updateActivated)
        {
            // Move the shark forward in the direction it's currently facing using Rigidbody2D
            Vector2 moveDirection = transform.up * speed;
            rb.velocity = moveDirection;
        }
        if (shouldChangeDirection)
        {
            GirarCuerpo girar = GetComponent<GirarCuerpo>();
            Destroy(girar);
            timeUntilChange += Time.deltaTime;
            if (timeUntilChange >= randomTime)
            {
                Debug.Log("Cambiando direccion");
                SetMoveDirection(0);
                gameObject.AddComponent<GirarCuerpo>();
                shouldChangeDirection = false;
            }
        }
    }
    
    // Adjust to set the shark's rotation based on the desired direction
    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }

}
