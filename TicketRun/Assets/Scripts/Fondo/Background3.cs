using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class Background3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public TextMeshProUGUI altitudeDisplay; // Assign TextMeshPro UI component for displaying altitude
    public float scrollSpeed = 0.1f; // Speed at which the background scrolls
    private Renderer bgRenderer;
     public Renderer additionalBgRenderer;
    private float lastTurtleY;
    private float totalAscent = 0.0f; // Variable to store the total ascent

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
                totalAscent += deltaY; // Add the vertical movement to the total ascent
                float offset = deltaY * scrollSpeed; // Determine the upward offset for the background

                // Update the texture offset by decreasing Y to scroll upwards
                bgRenderer.material.mainTextureOffset = new Vector2(bgRenderer.material.mainTextureOffset.x, bgRenderer.material.mainTextureOffset.y - offset);
                int totalAscent2 = Mathf.FloorToInt(totalAscent);

                // Update the altitude display to show the total ascent
                altitudeDisplay.text = $"{totalAscent2}";
                UpdateBackgroundColors(totalAscent);
            }

            // Always update lastTurtleY to the current Y position for the next frame
            lastTurtleY = currentTurtleY;
        }
    }
    void UpdateBackgroundColors(float ascent)
    {
        // Calculate the color based on ascent, adjusting the formula for a slower transition
        float progress = Mathf.Clamp01(ascent / 2000f);
        Color currentColor = Color.Lerp(bgRenderer.material.color, Color.white, progress * 0.1f); // Slow the rate of change

        // Apply the color to both backgrounds
        bgRenderer.material.color = currentColor;
        additionalBgRenderer.material.color = currentColor;
    }
}
