using UnityEngine;

public class LineaRecta : MonoBehaviour
{
    public float speed = 1.0f;

    void Update()
    {
        // Move the shark forward in the direction it's currently facing
        transform.position += transform.up * speed * Time.deltaTime;
    }
    
    // Adjust to set the shark's rotation based on the desired direction
    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }
}
