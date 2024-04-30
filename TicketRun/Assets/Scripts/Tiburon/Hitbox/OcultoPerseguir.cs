using System.Collections.Generic;
using UnityEngine;
public class OcultoPerseguir : MonoBehaviour
{
    public Oculto parentEnemy; // Reference to the parent Enemy script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            parentEnemy.UpdateObstacle(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            parentEnemy.ExitObstacle(other);
        }
    }
}
