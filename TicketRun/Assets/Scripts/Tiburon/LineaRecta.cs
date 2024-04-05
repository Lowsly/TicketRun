using UnityEngine;

public class LineaRecta : MonoBehaviour
{
    public float speed = 0.7f;
    public float difficulty;
    private Rigidbody2D rb;
    private bool shouldChangeDirection = false;
    private float timeUntilChange = 0, timeWhileChanging = 0;
    private float randomTime, randomChangeDirectionTime;
    GirarCuerpo girar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        girar = GetComponent<GirarCuerpo>();

        // Randomly decide whether to change direction based on difficulty
        float randomDecision = Random.Range(0, 2 + (difficulty -1)*4); 
        if (randomDecision > 3f)
        {
            shouldChangeDirection = true;
            randomTime = Random.Range(1f, 3f - difficulty/4);
            randomChangeDirectionTime = Random.Range(2f, 4f - difficulty/4);
        }
    }

    void Update()
    {
        // Move the shark forward in the direction it's currently facing using Rigidbody2D
        Vector2 moveDirection = transform.up * speed;
        rb.velocity = moveDirection;
        
        if (shouldChangeDirection)
        {
            timeUntilChange += Time.deltaTime;
            if (timeUntilChange >= randomTime)
            {
                if(girar != null)
                {
                    Destroy(girar);
                }
                timeWhileChanging += Time.deltaTime;
                Vector2 directionToZero = (Vector2.zero - (Vector2)transform.position).normalized;
                float targetAngleDegrees = Mathf.Atan2(directionToZero.y, directionToZero.x) * Mathf.Rad2Deg - 90; // Adjust for sprite orientation
                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngleDegrees);
                transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, timeWhileChanging / randomChangeDirectionTime);
                
                if (timeWhileChanging >= randomChangeDirectionTime)
                {
                    gameObject.AddComponent<GirarCuerpo>();
                    speed = speed * 1.2f + difficulty/4;
                    shouldChangeDirection = false;
                }
            }
        }
    }
    
    // Adjust to set the shark's rotation based on the desired direction
    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }

}
