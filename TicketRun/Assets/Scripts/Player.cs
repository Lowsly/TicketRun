using UnityEngine;

public class Player : MonoBehaviour
{
    public float upwardForce = 10f;
    public float gravityWhilePressed = 0.5f;
    private Rigidbody2D rb;
    private bool isPressed = false;
private float timer = 0f, timerFall = 0f, standardTimeWait=0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
            timer += Time.deltaTime;
            timerFall = 0f;
            if (timer <= standardTimeWait)
            {
                isPressed = true;
                rb.gravityScale = 0.2f;
            }
            if(timer > standardTimeWait)
            {
                isPressed = true;
                rb.gravityScale = gravityWhilePressed;
            }
        }
        else
        {
            timer = 0f;
            timerFall += Time.deltaTime;
            if(timerFall <= standardTimeWait)
            {
                rb.gravityScale = 0.8f;
            }
            if(timerFall > standardTimeWait)
            {
                rb.gravityScale = 0.9f;
            }
        }
    }
}
