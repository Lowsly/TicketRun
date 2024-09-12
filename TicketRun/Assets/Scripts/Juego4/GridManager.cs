using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int rows = 10; // Total number of rows
    public int columns = 10; // Total number of columns
    public GameObject obstaclePrefab; // Prefab for obstacles
    public float spawnDistance = 2f; // Minimum distance between obstacles
    public Transform playerTransform;

    private List<Vector2> gridPositions = new List<Vector2>();
    private Dictionary<Vector2, GameObject> spawnedObstacles = new Dictionary<Vector2, GameObject>(); // Track obstacles

    void Start()
    {
        GenerateInitialGrid();
    }

    void GenerateInitialGrid()
    {
        // Calculate the starting points for the grid to spread evenly around (0,0)
        int startX = -columns / 2;
        int startY = -rows / 2;

        for (int x = startX; x < startX + columns; x++)
        {
            for (int y = startY; y < startY + rows; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                gridPositions.Add(gridPosition);

                // Spawn obstacles with some logic
                if (Random.value > 0.7f) // Random chance to spawn an obstacle
                {
                    SpawnObstacle(gridPosition);
                }
            }
        }
    }

    public void GenerateNewGridRowsAndColumns(Vector2 playerDirection)
    {
        // Generate new grid rows or columns as the player moves
        if (playerDirection == Vector2.right)
        {
            AddColumn(Vector2.right);
            RemoveColumn(Vector2.left);
        }
        else if (playerDirection == Vector2.left)
        {
            AddColumn(Vector2.left);
            RemoveColumn(Vector2.right);
        }
        else if (playerDirection == Vector2.up)
        {
            AddRow(Vector2.up);
            RemoveRow(Vector2.down);
        }
        else if (playerDirection == Vector2.down)
        {
            AddRow(Vector2.down);
            RemoveRow(Vector2.up);
        }
    }

    void AddRow(Vector2 direction)
    {
        int newRow = (direction == Vector2.up) ? (rows / 2) : -(rows / 2 + 1);
        rows++;

        for (int x = -columns / 2; x < columns / 2; x++)
        {
            Vector2 newGridPosition = new Vector2(x, newRow);
            gridPositions.Add(newGridPosition);
            if (Random.value > 0.7f)
            {
                SpawnObstacle(newGridPosition);
            }
        }
    }

    void RemoveRow(Vector2 direction)
    {
        // Determine which row to remove based on the player's direction
        int rowToRemove = (direction == Vector2.up) ? (rows / 2 - 1) : -(rows / 2);

        for (int x = -columns / 2; x < columns / 2; x++)
        {
            Vector2 gridPosition = new Vector2(x, rowToRemove);
            gridPositions.Remove(gridPosition);
            RemoveObstacle(gridPosition);
        }

        rows--; // Decrement rows count
    }

    void AddColumn(Vector2 direction)
    {
        int newColumn = (direction == Vector2.right) ? (columns / 2) : -(columns / 2 + 1);
        columns++;

        for (int y = -rows / 2; y < rows / 2; y++)
        {
            Vector2 newGridPosition = new Vector2(newColumn, y);
            gridPositions.Add(newGridPosition);
            if (Random.value > 0.7f)
            {
                SpawnObstacle(newGridPosition);
            }
        }
    }

    void RemoveColumn(Vector2 direction)
    {
        // Determine which column to remove based on the player's direction
        int columnToRemove = (direction == Vector2.left) ? -(columns / 2) : (columns / 2 - 1);

        for (int y = -rows / 2; y < rows / 2; y++)
        {
            Vector2 gridPosition = new Vector2(columnToRemove, y);
            gridPositions.Remove(gridPosition);
            RemoveObstacle(gridPosition);
        }

        columns--; // Decrement columns count
    }

    void SpawnObstacle(Vector2 position)
    {
        if (Vector2.Distance(playerTransform.position, position) > spawnDistance)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
            spawnedObstacles[position] = obstacle; // Track the spawned obstacle
        }
    }

    void RemoveObstacle(Vector2 position)
    {
        if (spawnedObstacles.ContainsKey(position))
        {
            Destroy(spawnedObstacles[position]); // Destroy the obstacle GameObject
            spawnedObstacles.Remove(position); // Remove from the dictionary
        }
    }
}
