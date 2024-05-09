using UnityEngine;

public class Spawner3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public GameObject jumpPadPrefab; // Assign the prefab for the jump pad
    public Transform background; // Assign the background to limit X-axis spawn range
    public float ySpawnRange = 1.0f; // Y-axis range above the last jump pad to spawn the next pad
    public float xAvoidanceRange = 0.2f; // X-axis range to avoid around the last jump pad
    private float lastSpawnY; // Last Y position where a jump pad was spawned
    private float lastSpawnX; // Last X position where a jump pad was spawned

    void Start()
    {
        if (turtle != null)
        {
            // Initialize spawning at turtle's current position plus some offset
            lastSpawnY = turtle.transform.position.y;
            SpawnJumpPad(turtle.transform.position);
        }
    }

    void Update()
    {
        if (turtle.transform.position.y >= lastSpawnY + ySpawnRange - 1) // Check if turtle is about to reach the new altitude
        {
            SpawnJumpPad(new Vector3(Random.Range(background.position.x - background.localScale.x / 2, background.position.x + background.localScale.x / 2), lastSpawnY + ySpawnRange, 0));
        }
    }

    void SpawnJumpPad(Vector3 position)
    {
        float spawnX;

        do
        {
            // Generate a new X position within the bounds of the background
            spawnX = Random.Range(background.position.x - background.localScale.x / 2, background.position.x + background.localScale.x / 2);
        } while (Mathf.Abs(spawnX - lastSpawnX) < xAvoidanceRange); // Ensure it's outside the avoidance range of the last jump pad

        // Update the last spawn positions
        lastSpawnX = spawnX;
        lastSpawnY = position.y;

        // Create the new jump pad at the determined position
        Instantiate(jumpPadPrefab, new Vector3(spawnX, lastSpawnY, 0), Quaternion.identity);
    }
}
