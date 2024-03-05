using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public float spawnRate = 2f; // Rate of spawn (objects per second)
    public float spawnDistance = 10f; // Distance from the edge of the screen where objects will spawn
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnObject", 0f, 1f / spawnRate); // Start spawning objects at specified rate
    }

    void SpawnObject()
    {
        // Determine spawn position
        Vector3 spawnPosition = Vector3.zero;
        bool spawnFromTop = Random.Range(0, 2) == 0; // Randomly choose whether to spawn from top or bottom
        if (spawnFromTop)
        {
            spawnPosition = new Vector3(Random.Range(0f, mainCamera.pixelWidth)+mainCamera.pixelWidth*2, mainCamera.pixelHeight + spawnDistance, 10f);
        }
        else
        {
            spawnPosition = new Vector3(Random.Range(0f, mainCamera.pixelWidth)+mainCamera.pixelWidth*2, -spawnDistance, 10f);
        }
        spawnPosition = mainCamera.ScreenToWorldPoint(spawnPosition);

        // Spawn object
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}