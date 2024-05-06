using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public float moveSpeed, animatorSpeed = 0; 
    public Image[] healthBars;
    public AudioClip audioClipDamage, audioClipHearth, audioClipDeath;
    private AudioSource audioSource;

    public Sprite fullBar, emptyBar;
    public int maxHealth = 5, currentHealth, _money; 
    public Collider2D backgroundCollider, UICollider;
    public GameObject player, spawner;
    public bool _immune,dead;
    private SpriteRenderer _renderer;
    private Animator _animator;
    public FixedJoystick joystick;
    private Rigidbody2D rb;
    private Vector2 move;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateHealthUI();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(PlayerPrefs.GetInt("joystickEnabled", 0) == 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButton(0)  && EventSystem.current.currentSelectedGameObject == null ) {
                if(backgroundCollider.OverlapPoint(mousePosition) && !UICollider.OverlapPoint(mousePosition))
                {
                    transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
                }
                else 
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(mousePosition.x, UICollider.transform.position.y - UICollider.transform.localScale.y / 2), moveSpeed * Time.deltaTime);
                }
                if(mousePosition.y>=transform.position.y-0.1f)
                {
                    _animator.SetFloat("Speed", 1.2f + animatorSpeed);
                }
                else 
                {
                    _animator.SetFloat("Speed", 0.4f);
                }
                    
                    
            }
            else 
                _animator.SetFloat("Speed", 0.9f + animatorSpeed);

        }
        else if(PlayerPrefs.GetInt("joystickEnabled", 0) == 1)
        {
            move.x = joystick.Horizontal;
            move.y = joystick.Vertical;
        }
        
    }
    private void FixedUpdate (){
        rb.MovePosition(rb.position + move * moveSpeed/15 * Time.fixedDeltaTime);
    }
     private void OnTriggerStay2D(Collider2D other)
    {
        if(!dead)
        {
            if(!_immune)
            {
                if (other.CompareTag("Enemy") || other.CompareTag("Obstacle") || other.CompareTag("Basura"))
                {
                    TakeDamage(1);
                }
            }
        
            if (other.CompareTag("Healing"))
            {
                Destroy(other.gameObject);
                Heal(1);
            }
        }
        
    }
    public void TakeDamage(int damage)
    {
        if(!_immune){
            StartCoroutine(HeartColor());
            StartCoroutine(Immune());
            StartCoroutine(Damaged());
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                dead = true;
                player.layer = LayerMask.NameToLayer("Immune");
                StartCoroutine(Death());
            }
            
            if (audioClipDamage != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioClipDamage);
            }
        }
    }
     IEnumerator HeartColor()
    {
        int numberOfFlashes = 2;

        for (int flash = 0; flash < numberOfFlashes; flash++)
        {
            // Disable the heart images up to the current health
            for (int i = 0; i < maxHealth; i++)
            {
                healthBars[i].enabled = false;
            }

            // Wait for a short time (e.g., 0.1 seconds)
            yield return new WaitForSecondsRealtime(0.1f);

            // Enable the heart images up to the current health
            for (int i = 0; i < maxHealth; i++)
            {
                healthBars[i].enabled = true;
            }

            // Wait for the same short time before the next flash
            yield return new WaitForSecondsRealtime(0.1f);
        }
        UpdateHealthUI();
        yield return null;
    }

     IEnumerator Immune()
    {
        _immune=true;
        yield return new WaitForSeconds(1.5f);
        _immune=false;
        yield return null;
    }

    IEnumerator Damaged()
    {
        for (int i = 0; i < 6; i++)
            {
             _renderer.color = new Color (0, 0, 0, 0f);
             yield return new WaitForSecondsRealtime(.1f);
             _renderer.color = Color.white;
             yield return new WaitForSecondsRealtime(.1f);
            }
        
    }
    void UpdateHealthUI()
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (i < maxHealth)
            {
                healthBars[i].enabled = true; // Activa la barra de vida
            }
            else
            {
                healthBars[i].enabled = false; // Desactiva barras de vida adicionales
            }
        }
        for (int i = 0; i <maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthBars[i].sprite = fullBar; // Activa la barra de vida
            }
            else
            {
                healthBars[i].sprite = emptyBar; // Desactiva barras de vida adicionales
            }
        }
    }

     public void Heal(int healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (audioClipHearth != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClipHearth);
        }
        UpdateHealthUI();
    }
    IEnumerator Death()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        Spawner spawnerScript = spawner.GetComponent<Spawner>();
        spawnerScript.Dead();

        if (audioClipDeath != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClipDeath);
        }

    }
    

}