using UnityEngine;
using TMPro;
using System.Collections;

public class SpawnerJuego2 : MonoBehaviour
{
    public Transform background;
    public float timeToSpawn;
    private float timeSinceLastSpawn = 4;
    public GameObject prefab;
    public int points = 0;
    private float _bh,_bw;
    public TextMeshProUGUI pointsCount, time, bestTime;
    public GameObject gameOver,pauseButton, pauseMenu, optionsMenu;  
    private bool isAlive = true;
    void Start()
    {
        _bh = background.transform.localScale.y;
        _bw = background.transform.localScale.x;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn && isAlive)
        {
            GameObject rock = Instantiate(prefab, new Vector2(_bw/1.5f, Random.Range(-_bh/3f,_bh/3.2f)), Quaternion.identity);
            rock.GetComponent<Rocas>().points = points;
            timeSinceLastSpawn = 0;
        }
    }
    public void UpdatePoints()
    {
        points+=1;
        pointsCount.text = "" + points; 
    }
    public IEnumerator Dead()
    {
        isAlive= false;
        yield return new WaitForSeconds(0.9f);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
        optionsMenu.SetActive(false);
        gameOver.SetActive(true);     
        PlayerPrefs.SetInt("Points", points);
        time.text = "Puntuación actual: " +  points;
        if(points > PlayerPrefs.GetInt("BestPoints",0))
        {
            PlayerPrefs.SetInt("BestPoints", points);
            PlayerPrefs.Save();
            bestTime.text = "Mejor puntuación: " + points;
        }
        else
        {
            bestTime.text = "Mejor puntuación: " +  PlayerPrefs.GetInt("BestPoints",0);
        }
        yield return null;
        
    }
}
