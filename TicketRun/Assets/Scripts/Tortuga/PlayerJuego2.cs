using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJuego2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float upliftForce = 5.0f; 
    private bool isDead, ready;
    public AudioClip success, audioClipDeath;
    private AudioSource audioSource;
    public SpawnerJuego2 spawner;
    public Animator animator;
    public GameObject presiona;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>();
        animator.SetFloat("speed",0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ready && EventSystem.current.currentSelectedGameObject == null)
        {
            ready = true;
            rb.gravityScale = 0.9f;
            spawner.gameObject.SetActive(true);
            animator.SetFloat("speed",1f);
            presiona.SetActive(false);
        }
        if(ready)
        {
            if (Input.GetMouseButtonDown(0) && !isDead && EventSystem.current.currentSelectedGameObject == null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, upliftForce), ForceMode2D.Impulse);
            }
        }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isDead = true;
            animator.SetFloat("speed",0);
            gameObject.transform.localScale = new Vector3(-0.15f, -0.15f, 0);
            gameObject.layer = LayerMask.NameToLayer("Immune");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 1.6f;
            rb.AddForce(new Vector2(0, upliftForce*2), ForceMode2D.Impulse);
            StartCoroutine(spawner.Dead());
            audioSource.PlayOneShot(audioClipDeath);
        }
        if(collision.CompareTag("Healing"))
        {
            audioSource.PlayOneShot(success);
            spawner.UpdatePoints();
            Destroy(collision.gameObject);
        }
    }
}