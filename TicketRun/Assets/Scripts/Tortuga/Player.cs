using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed; 
    public Image[] healthBars;

    public Sprite fullBar, emptyBar;
    public int maxHealth = 5, currentHealth, _money; 
    public Collider2D backgroundCollider, UICollider;
    public GameObject player, spawner, gameOver,pauseButton, pauseMenu;
    public bool _immune,dead;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private void Start()
    {
        UpdateHealthUI();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
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
            float distance1 = Mathf.Abs(UICollider.transform.position.y - transform.position.y);
                _animator.SetFloat("Speed", 1.3f);
                
        }
        else
            _animator.SetFloat("Speed", 1);

    }

     private void OnTriggerEnter2D(Collider2D other)
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
        UpdateHealthUI();
    }
    IEnumerator Death()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        Destroy(spawner);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
        gameOver.SetActive(true); 
        

    }

}