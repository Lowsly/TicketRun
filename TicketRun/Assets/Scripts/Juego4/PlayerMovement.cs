using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gridSize = 1f; // Size of one grid unit
    private Vector2 currentGridPosition;

    void Start()
    {
        currentGridPosition = transform.position;
    }

    public void Move(Vector2 direction)
    {
        Vector2 targetPosition = currentGridPosition + direction * gridSize;
        currentGridPosition = targetPosition;
        transform.position = targetPosition;

        // Trigger new grid generation based on the new position
        FindObjectOfType<GridManager>().GenerateNewGridRowsAndColumns(direction);
    }
}
