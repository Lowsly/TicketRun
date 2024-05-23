using UnityEngine;

public class Juego3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public float moveSpeed = 0.5f; // How responsive the game object is to the turtle's upward movement
    private Vector3 lastTurtlePosition; // To track changes in the turtle's position
    private Animator anim;

    void Start()
    {
        if (turtle != null)
        {
            lastTurtlePosition = turtle.transform.position; // Initialize with the turtle's starting position
        }
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (turtle != null)
        {
            // Calculate how much the turtle has moved since the last frame
            Vector3 turtleMovement = turtle.transform.position - lastTurtlePosition;

            // If the turtle has moved up (turtleMovement.y > 0), calculate the new position
            if (turtleMovement.y > 0)
            {
                // Calculate the downward movement for the GameObject
                float newYPosition = transform.position.y - (turtleMovement.y * moveSpeed);

                // Apply the new y position if it is lower than the current y position
                if (newYPosition < transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
                }
            }

            // Update the last turtle position for the next frame's calculation
            lastTurtlePosition = turtle.transform.position;
        }
    }

    public void setTurtle(GameObject turtle)
    {
        this.turtle = turtle; // Set the turtle reference directly
    }
    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(anim !=null)
        anim.SetTrigger("Salto");
    }
}
