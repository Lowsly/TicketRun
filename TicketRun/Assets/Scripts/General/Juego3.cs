using UnityEngine;

public class Juego3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public float range = 1.0f; // The range the turtle needs to exceed the previous maximum altitude to trigger pad movement
    private float maximumAltitude; // To track the highest altitude reached by the turtle

    void Start()
    {
        if (turtle != null)
        {
            // Initialize the maximum altitude with the turtle's starting position
            maximumAltitude = turtle.transform.position.y;
        }
    }

    void Update()
    {
        if (turtle != null)
        {
            // Check if the turtle has reached a new maximum altitude exceeding the set range
            if (turtle.transform.position.y > maximumAltitude + range)
            {
                // Calculate how much the pads need to move down
                float moveDownAmount = turtle.transform.position.y - (maximumAltitude + range);
                
                // Update the maximum altitude to the new altitude
                maximumAltitude = turtle.transform.position.y;

                // Move the jump pads down by the calculated amount
                transform.position = new Vector3(transform.position.x, transform.position.y - moveDownAmount, transform.position.z);
            }
        }
    }
}
