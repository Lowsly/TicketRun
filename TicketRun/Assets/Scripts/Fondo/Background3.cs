using UnityEngine;

public class Background3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public float scrollSpeed = 0.1f; // Speed at which the background scrolls
    private Renderer bgRenderer;
    private float lastTurtleY;

    void Start()
    {
        bgRenderer = GetComponent<Renderer>(); // Get the Renderer component
        if (turtle != null)
        {
            lastTurtleY = turtle.transform.position.y; // Initialize with the turtle's starting Y position
        }
    }

    void Update()
    {
        if (turtle != null)
        {
            float currentTurtleY = turtle.transform.position.y;
            if (currentTurtleY > lastTurtleY) // Check if the turtle has moved up since the last frame
            {
                float deltaY = currentTurtleY - lastTurtleY; // Calculate how much the turtle has moved up
                float offset = deltaY * scrollSpeed; // Determine the upward offset for the background

                // Get the current texture offset
                Vector2 currentOffset = bgRenderer.material.mainTextureOffset;

                // Update the texture offset by decreasing Y to scroll upwards
                bgRenderer.material.mainTextureOffset = new Vector2(currentOffset.x, currentOffset.y - offset);
            }

            // Always update lastTurtleY to the current Y position for the next frame
            lastTurtleY = currentTurtleY;
        }
    }
}
