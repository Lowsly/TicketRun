using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private float[] timeToSpawn = { 0.54f, 7, 1.5f, 10, 20 };
    private float[] timeSinceLastSpawn = { 0, 0, 0, 1, 1 };

    public float difficulty = 1f;
    private float softCapDifficulty = 5F;
    public float difficultyIncreaseRate = 0.05f;
    private float difficultyIncreaseInterval = 2.5f;
    private float timeSinceLastDifficultyIncrease = 0.0f;
    public Transform background, objectZone;
    public Player player;
    public Background backgroundSpeed;
    private Dictionary<float, float> yCooldowns = new Dictionary<float, float>();
    private float yRangeCooldown = 10.0f; // Cooldown in seconds for Y ranges
    private float allowedYRange = 0.2f; // This is the range within which Y values are considered the same for spawning purposes
    private float timeAlive;
    private bool isAlive = true;
    private float _bh, _bw, bw, _ozw, _ozh;
    public GameObject gameOver,pauseButton, pauseMenu, optionsMenu, newRecord;
    public TextMeshProUGUI time, bestTime;
    
    void Start()
    {
        _bh = background.transform.localScale.y;
        _bw = background.transform.localScale.x;
        _ozw = objectZone.transform.localScale.x/2;
        _ozh = objectZone.transform.localScale.y/2;
        bw = _bw/2;
    }

    void Update()
    {
        if(isAlive)
        {
            UpdateSpawns();
            IncreaseDifficulty();
            timeAlive+=Time.deltaTime;
        }
    }

    void UpdateSpawns()
    {
        for (int i = 0; i < timeSinceLastSpawn.Length; i++)
        {
            timeSinceLastSpawn[i] += Time.deltaTime;
            if (timeSinceLastSpawn[i] >= timeToSpawn[i]-difficulty/7)
            {
                switch (i)
                {
                    case 0: GenerateShark(); break;
                    case 1: GenerateBoat(); break;
                    case 2: GenerateTrash(); break;
                    case 3: GenerateEgg(); break;
                    case 4: GenerateLife(); break;
                }
                timeSinceLastSpawn[i] = 0;
            }
        }
    }
    public void Dead()
    {
        isAlive= false;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
        gameOver.SetActive(true); 
        optionsMenu.SetActive(false);
        PlayerPrefs.SetInt("TimeAliveCurrent", Mathf.FloorToInt(timeAlive));
        time.text = "Tiempo sobrevivido: " +  Mathf.FloorToInt(timeAlive) + "s";
        if(timeAlive > PlayerPrefs.GetInt("TimeAliveMax",0))
        {
            if(PlayerPrefs.GetInt("FirsTime, 0 ") >= 1)
            {   
                newRecord.SetActive(true);
            }
            PlayerPrefs.SetInt("TimeAliveMax", Mathf.FloorToInt(timeAlive));
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
            bestTime.text = "Mejor tiempo: " + timeAlive + "s";
        }
        else
        {
            bestTime.text = "Mejor tiempo: " +  PlayerPrefs.GetInt("TimeAliveMax",0) + "s";
        }
        
    }
    void GenerateTrash()
    {
        float RandX = Random.Range(0, 2) == 0 ? -bw-0.19f : bw+0.19f;
        int direction = RandX < 0 ? 0 : 180;
        float yCoord;
        do
        {
            yCoord = Random.Range(-_bh / 2, (_bh - 0.3f) / 2);
        } while (!IsYCoordAvailable(yCoord));

        yCooldowns[yCoord] = Time.time + yRangeCooldown; // Update cooldown for the Y coordinate

        // Create the trash GameObject at the calculated position and rotation.
        GameObject trash = Instantiate(prefabs[2], new Vector2(RandX, yCoord), Quaternion.Euler(0, 0, direction));
    }
    bool IsYCoordAvailable(float yCoord)
    {
        foreach (var entry in new List<float>(yCooldowns.Keys))
        {
            if (Time.time > yCooldowns[entry])
            {
                yCooldowns.Remove(entry); // Cleanup expired cooldowns
            }
            else if (Mathf.Abs(entry - yCoord) <= allowedYRange)
            {
                return false; // Y coordinate is within a "blocked" range
            }
        }
        return true;
    }

     void GenerateShark()
    {
        GameObject SharkLine = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
        Enemy enemy = SharkLine.GetComponent<Enemy>();
        int spawnQuadrant = DetermineSpawnLocation(SharkLine.transform.position); 
        float angleDegrees = DetermineAngle(spawnQuadrant);
        enemy.SetMoveDirection(angleDegrees);
        enemy.speed =  0.75f + difficulty/10;
        enemy.turnSpeed = 78.5f + difficulty*1.5f;
        if(Random.Range(0,25) == 0 && difficulty > 1.2f)
        {
            GameObject SharkChase1 = Instantiate(prefabs[3], RandomPosition(), Quaternion.identity);
            Perseguir perseguir = SharkChase1.GetComponent<Perseguir>();
            perseguir.speed =   0.75f + difficulty/9;
            perseguir.turnSpeed = 1.5f + difficulty/2;
            perseguir.chaseDuration = 6 + difficulty;
        }
        if(Random.Range(0,40) == 0 && difficulty > 1.3f)
        {
            GameObject SharkChase2 = Instantiate(prefabs[4], RandomPosition(), Quaternion.identity);
            Oculto oculto = SharkChase2.GetComponent<Oculto>();
            oculto.speed = 0.8f + difficulty/11;
            oculto.turnSpeed = difficulty;
            oculto.chaseDuration = 7 + difficulty;
        }
    }

    void GenerateBoat()
    {
        float Rand =  Random.Range(0, 2) == 0 ? -(_bh/2)-0.6f : (_bh/2)+0.6f;

        int direction = Rand > 0 ? 180 : 0;
       
        GameObject Boat = Instantiate(prefabs[1], new Vector2(Random.Range(-bw, bw), Rand),Quaternion.Euler(new Vector3(0, 0, direction)));
        Bote bote = Boat.GetComponent<Bote>();
        bote.speed = 0.6f + difficulty/10;
        bote.turnSpeed = 29 + difficulty;

    }
    void GenerateEgg()
    {  
        Instantiate(prefabs[5], new Vector2(Random.Range(-_ozw, _ozw), Random.Range(-_ozh, _ozh)), Quaternion.identity);
    }
    void GenerateLife()
    {  
        Instantiate(prefabs[6], new Vector2(Random.Range(-_ozw, _ozw), Random.Range(-_ozh, _ozh)), Quaternion.identity);
    }
    void IncreaseDifficulty()
    {
        timeSinceLastDifficultyIncrease += Time.deltaTime;
        if (timeSinceLastDifficultyIncrease >= difficultyIncreaseInterval)
        {
            difficulty = Mathf.Min(difficulty + difficultyIncreaseRate, softCapDifficulty);
            timeSinceLastDifficultyIncrease = 0;
            backgroundSpeed.scrollSpeed = difficulty/10;
            player.animatorSpeed = difficulty/10;
        }
    }

    Vector3 RandomPosition()
    {
        float Rand =  Random.Range(0, 2) == 0 ? -(_bh/2)-0.4f : (_bh/2)+0.4f;
        return new Vector3(Random.Range(-bw, bw), Rand, 0);
    }



    float DetermineAngle(int spawnQuadrant)
    {
        float angleDegrees;
         switch (spawnQuadrant)
            {
                case 0: // Top-Left
                    angleDegrees = Random.Range(-115, -165); 
                    break;
                case 1: // Top-Right
                    angleDegrees = Random.Range(115, 165); 
                    break;
                case 2: // Bottom-Left
                    angleDegrees = Random.Range(-15, -75); 
                    break;
                case 3: // Bottom-Right
                    angleDegrees = Random.Range(15, 75); 
                    break;
                case 4: //Top-side
                    angleDegrees = Random.Range(155, 205); 
                    break;
                case 5: // Bottom Side
                    angleDegrees = Random.Range(25, -25); 
                    break;
                case 6: // Left Side
                    angleDegrees = Random.Range(-65, -115); 
                    break;
                case 7: // Right Side
                    angleDegrees = Random.Range(65, 115); 
                    break;
                default:
                    angleDegrees = Random.Range(0, 360); 
                    break;
            }
        return angleDegrees;
    }
    int DetermineSpawnLocation(Vector3 position)
    {
        float cornerThresholdX = _bw / 3; // Adjust as needed for your game's design.
        float cornerThresholdY = _bh / 4; // Adjust as needed.

        bool nearLeftOrRightEdge = Mathf.Abs(position.x) > (_bw / 2) - cornerThresholdX;
        bool nearTopOrBottomEdge = Mathf.Abs(position.y) > (_bh / 2) - cornerThresholdY;

        // Determine corners
        if (nearLeftOrRightEdge && nearTopOrBottomEdge)
        {
            if (position.x <= 0 && position.y > 0) return 0; // Top-Left Corner
            if (position.x > 0 && position.y > 0) return 1; // Top-Right Corner
            if (position.x <= 0 && position.y <= 0) return 2; // Bottom-Left Corner
            if (position.x > 0 && position.y <= 0) return 3; // Bottom-Right Corner
        }
        // Determine sides
        else if (nearTopOrBottomEdge)
        {
            if (position.y > 0) return 4; // Top Side
            else return 5; // Bottom Side
        }
        else if (nearLeftOrRightEdge)
        {
            if (position.x <= 0) return 6; // Left Side
            else return 7; // Right Side
        }

        return 8; // Default case, possibly center or not fitting other criteria
    }

}
