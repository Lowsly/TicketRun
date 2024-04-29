using UnityEngine;

public class DetectarObstaculosBote : MonoBehaviour
{
    public Bote parentEnemy; // Reference to the parent Enemy script
    private Collider2D parentCollider; // Collider of the parent

    private void Start()
    {
        // Get the Collider2D component from the parent
        parentCollider = parentEnemy.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is not the parent's collider
        if (other.CompareTag("Obstacle") && other != parentCollider)
        {
            parentEnemy.HandleObstacleEnter(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Again, check if the collider is not the parent's collider
        if (other.CompareTag("Obstacle") && other != parentCollider)
        {
            parentEnemy.HandleObstacleExit(other.transform);
        }
    }
}
