using UnityEngine;

public class EscuelaSpawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of objects to spawn
    public float minDelay = 1f; // Minimum delay between spawns
    public float maxDelay = 3f; // Maximum delay between spawns

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnObjects());
    }

    System.Collections.IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Randomly choose an object from the array
            GameObject objectToInstantiate = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            // Instantiate the selected object at the spawner's position
            Instantiate(objectToInstantiate, transform.position, Quaternion.identity);

            // Randomly choose the delay before the next spawn
            float delay = Random.Range(minDelay, maxDelay);

            // Wait for the specified delay before spawning the next object
            yield return new WaitForSeconds(delay);
        }
    }
}
