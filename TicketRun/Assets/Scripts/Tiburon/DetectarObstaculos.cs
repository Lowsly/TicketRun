using System.Collections.Generic;
using UnityEngine;
public class DetectarObstaculos : MonoBehaviour
{
    public Enemy parentEnemy; // Reference to the parent Enemy script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyObstacle"))
        {
            Debug.Log("Early warning obstacle detected by detector: " + other.name);
            parentEnemy.HandleEarlyWarningEnter(other.transform);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Direct obstacle detected by detector: " + other.name);
            parentEnemy.HandleObstacleEnter(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EnemyObstacle"))
        {
            parentEnemy.HandleEarlyWarningExit(other.transform);
        }
        else if (other.CompareTag("Obstacle"))
        {
            parentEnemy.HandleObstacleExit(other.transform);
        }
    }
}
