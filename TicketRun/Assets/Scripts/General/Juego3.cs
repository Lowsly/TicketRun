using UnityEngine;

public class Juego3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public float moveSpeed = 0.5f; // How responsive the background is to the turtle's movement
    private Vector3 lastTurtlePosition; // To track changes in the turtle's position

    void Start()
    {
        if (turtle != null)
        {
            lastTurtlePosition = turtle.transform.position; // Initialize with the turtle's starting position
        }
    }

    void Update()
    {
        if (turtle != null)
        {
            Vector3 turtleMovement = turtle.transform.position - lastTurtlePosition; // Determine how much the turtle has moved
            Vector3 moveDelta = new Vector3(turtleMovement.x * moveSpeed, turtleMovement.y * moveSpeed, 0); // Scale the movement

            // Move the background inversely to the turtle's movement
            transform.position -= moveDelta;

            // Update the last position for the next frame's calculation
            lastTurtlePosition = turtle.transform.position;
        }
    }
    public void setTurtle(GameObject turtle)
    {
        this.turtle = turtle; // You had a logic error here; it should set `turtle` without checking if it's null.
    }
}
