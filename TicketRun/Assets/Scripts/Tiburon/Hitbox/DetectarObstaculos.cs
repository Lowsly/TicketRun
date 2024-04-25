using System.Collections.Generic;
using UnityEngine;

public class DetectarObstaculos : MonoBehaviour
{
    public Enemy parentEnemy; // Reference to the parent Enemy script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Basura"))
        {
            parentEnemy.HandleObstacleEnter(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Basura"))
        {
            parentEnemy.HandleObstacleExit(other.transform);
        }
    }
}
