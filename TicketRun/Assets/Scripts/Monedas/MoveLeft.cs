using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f; // Constant speed for leftward movement

    void Update()
    {
        // Move the object to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
