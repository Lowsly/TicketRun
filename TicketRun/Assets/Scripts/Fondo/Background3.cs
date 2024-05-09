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
            float deltaY = turtle.transform.position.y - lastTurtleY; // Calculate how much the turtle has moved since the last frame
            float offset = deltaY * scrollSpeed; // Determine the offset for the background

            // Get the current texture offset
            Vector2 currentOffset = bgRenderer.material.mainTextureOffset;

            // Update the texture offset with the new value, moving inversely to the turtle
            bgRenderer.material.mainTextureOffset = new Vector2(currentOffset.x, currentOffset.y - offset);

            // Update lastTurtleY to the new Y position for the next frame
            lastTurtleY = turtle.transform.position.y;
        }
    }
}
