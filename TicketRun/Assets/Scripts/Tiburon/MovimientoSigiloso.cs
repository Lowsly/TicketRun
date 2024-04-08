using UnityEngine;

public class MovimientoSigiloso : MonoBehaviour
{
    public float speed = 0.5f, angleSpeed = 0.5f; // Reduced movement speed for slower rotation
    private Rigidbody2D rb;
    private bool isOrbiting = false;
    private float orbitStartTime = 0.0f; // Time before starting to orbit in seconds
    private float orbitDuration, chaseTime; // Duration of orbiting in seconds
    private float timeOrbiting = 0f; // Time spent orbiting
    public Transform centerPoint; // Assigned to the turtle
    private float orbitRadius; // Adjusted to reduce distance
    private float orbitAngle; // Current angle of orbiting
    private bool bait, aNewSpeed;
    private SpriteRenderer spriteRenderer;
    private GameObject Hitbox;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Hitbox = gameObject.transform.GetChild(0).gameObject;
        orbitDuration = Random.Range(10f, 15f);
        chaseTime = orbitDuration;
        Invoke("StartOrbiting", orbitStartTime);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color tmp = spriteRenderer.color;
        tmp.a = 0.5f;
        spriteRenderer.color = tmp;
        Tocado tocado = Hitbox.GetComponent<Tocado>();
        Destroy(tocado);
    }

    void FixedUpdate()
    {
        if (isOrbiting)
        {
            timeOrbiting += Time.fixedDeltaTime;
            if (timeOrbiting >= orbitDuration * 0.5f && timeOrbiting <= orbitDuration)
            {
                float alphaTransitionTime = orbitDuration * 0.25f; // Duration of the alpha transition
                float alphaProgress = (timeOrbiting - orbitDuration * 0.75f) / alphaTransitionTime;
                    Color tmp = spriteRenderer.color;
                    tmp.a = Mathf.Lerp(0.5f, 1.0f, alphaProgress); // Lerp alpha from 0.5 to 1
                    spriteRenderer.color = tmp;
            }
            if (timeOrbiting >= orbitDuration * 0.75f)
            {
                if(Hitbox.GetComponent<Tocado>() == null)
                Hitbox.AddComponent<Tocado>();
            }
            if (timeOrbiting <= orbitDuration)
            {
                // Perform orbiting with adjusted parameters
                orbitAngle += speed / orbitRadius * Time.fixedDeltaTime; // Adjust angle change rate based on radius
                Vector2 orbitPosition = new Vector2(
                    Mathf.Cos(orbitAngle) * orbitRadius + centerPoint.position.x,
                    Mathf.Sin(orbitAngle) * orbitRadius + centerPoint.position.y);

                // Face the direction of the next point in the orbit
                Vector2 direction = orbitPosition - (Vector2)transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                rb.rotation = angle;

                // Continue moving forward
               
            }
            else if(bait)
            {
                // After orbiting, smoothly rotate to face the turtle and maintain forward movement
                Vector2 directionToCenter = (centerPoint.position - transform.position).normalized;
                float targetAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.fixedDeltaTime * angleSpeed);
                setSpeed(1.2f);
           
            }
            else
            {
                setSpeed(1.1f);
                    Vector2 directionToZero = (Vector2.zero - (Vector2)transform.position).normalized;
                    float targetAngle = Mathf.Atan2(directionToZero.y, directionToZero.x) * Mathf.Rad2Deg - 90;
                    Quaternion currentRotation = transform.rotation;
                    Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                    transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Random.Range(0.025f, 0.04f));
                    
                
           
            }
        }
        
        
            // Initial movement towards orbit start position
            rb.velocity = transform.up * speed;
        
    }

    void StartOrbiting()
    {
        isOrbiting = true;
        // Set a closer orbit radius
        orbitRadius = Vector2.Distance(transform.position, centerPoint.position) * Random.Range(0.5f,0.4f); // Scale down for closer orbit
        orbitAngle = Mathf.Atan2(transform.position.y - centerPoint.position.y, transform.position.x - centerPoint.position.x);
    }

    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }
    void setSpeed(float speed2)
    {
        if(!aNewSpeed)
        speed*=speed2;
        aNewSpeed = true;
    }
}
