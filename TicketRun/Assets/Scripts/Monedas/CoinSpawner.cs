using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // The prefab of the coin
    public CoinPattern[] patterns; // Array of coin patterns to spawn
    private int currentPatternIndex = 0; // Index of the current pattern being used
    private Transform spawnPoint; // Spawn point for the coins

    void Start()
    {
        spawnPoint = transform; // Set the spawn point to the position of the GameObject this script is attached to
        SpawnCoins();
    }

    void SpawnCoins()
    {
        // Get the current pattern
        CoinPattern currentPattern = patterns[currentPatternIndex];

        // Iterate through the positions in the pattern and spawn coins
        foreach (Vector3 position in currentPattern.positions)
        {
            Instantiate(coinPrefab, spawnPoint.position + position, Quaternion.identity);
        }

        // Move to the next pattern
        currentPatternIndex = (currentPatternIndex + 1) % patterns.Length;
    }
}