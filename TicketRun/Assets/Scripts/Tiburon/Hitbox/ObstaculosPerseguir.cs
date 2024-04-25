using System.Collections.Generic;
using UnityEngine;
public class ObstaculosPerseguir : MonoBehaviour
{
    public Perseguir parentEnemy; // Reference to the parent Enemy script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyObstacle"))
        {
            parentEnemy.HandleEarlyWarningEnter(other.transform);
        }
        else if (other.CompareTag("Obstacle"))
        {
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
