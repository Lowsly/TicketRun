using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private float[] timeToSpawn = { 0.5f, 13, 2 };
    private float[] timeSinceLastSpawn = { 2, 14, 2 };

    public float difficulty = 1f;
    private float softCapDifficulty = 1.5F;
    public float difficultyIncreaseRate = 0.025f;
    private float difficultyIncreaseInterval = 2.5f;
    private float timeSinceLastDifficultyIncrease = 0.0f;

    public Transform playerTransform;
    public Transform background;

    private Dictionary<float, float> yCooldowns = new Dictionary<float, float>();
    private float yRangeCooldown = 10.0f; // Cooldown in seconds for Y ranges
    private float allowedYRange = 0.1f; // This is the range within which Y values are considered the same for spawning purposes

    private float _bh, _bw, bw;
    
    void Start()
    {
        _bh = background.transform.localScale.y;
        _bw = background.transform.localScale.x;
        bw = _bw/2;
    }

    void Update()
    {
        UpdateSpawns();
        IncreaseDifficulty();
    }

    void UpdateSpawns()
    {
        for (int i = 0; i < timeSinceLastSpawn.Length; i++)
        {
            timeSinceLastSpawn[i] += Time.deltaTime;
            if (timeSinceLastSpawn[i] >= timeToSpawn[i]-difficulty/3)
            {
                switch (i)
                {
                    case 0: GenerateShark(); break;
                    case 1: GenerateBoat(); break;
                    case 2: GenerateTrash(); break;
                }
                timeSinceLastSpawn[i] = 0;
            }
        }
    }

    void GenerateTrash()
{
    float RandX = Random.Range(0, 2) == 0 ? -bw-0.4f : bw+0.4f;
    int direction = RandX < 0 ? 0 : 180;
    float yCoord;
    do
    {
        yCoord = Random.Range(-_bh / 2, _bh / 2);
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
    }

    void GenerateBoat()
    {
        float Rand =  Random.Range(0, 2) == 0 ? -(_bh/2)-0.4f : (_bh/2)+0.4f;

        int direction = Rand > 0 ? 180 : 0;
       
        GameObject Boat = Instantiate(prefabs[1], new Vector2(Random.Range(-bw, bw), Rand),Quaternion.Euler(new Vector3(0, 0, direction)));

    }
    void IncreaseDifficulty()
    {
        timeSinceLastDifficultyIncrease += Time.deltaTime;
        if (timeSinceLastDifficultyIncrease >= difficultyIncreaseInterval)
        {
            difficulty = Mathf.Min(difficulty + difficultyIncreaseRate, softCapDifficulty);
            timeSinceLastDifficultyIncrease = 0;
        }
    }
     void GenerateSharkv1()
    {
        float bw = _bw/2;
        float Rand =  Random.Range(0, 2) == 0 ? -(_bh/2)-0.4f : (_bh/2)+0.4f;

        int direction = Rand > 0 ? 180 : 0;
       
        GameObject Boat = Instantiate(prefabs[0], new Vector2(Random.Range(-bw, bw), Rand),Quaternion.Euler(new Vector3(0, 0, direction)));

        float RandX =  Random.Range(0, 2) == 0 ? -bw-0.4f : bw+0.4f;

        int direction2 = RandX > 0 ? 90 : -90;
        
        GameObject Trash = Instantiate(prefabs[1], new Vector2(RandX,Random.Range(-_bh/2, _bh/2)), Quaternion.Euler(new Vector3(0, 0, direction2)));
        
        /*if(Random.Range(0, 3) == 0 && difficulty > softCapDifficulty)
        {
            GameObject SharkLine2 = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
            AssignSharkBehaviorLine(SharkLine2);
        }
        if(Random.Range(0, 4) == 0 && difficulty > softCapDifficulty)
        {
            GameObject SharkSneaky = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
            AssignSharkSneaky(SharkSneaky);
        }
        GameObject SharkChase = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
        AssignSharkBehaviorChase(SharkChase);
        GameObject SharkLine = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
        AssignSharkBehaviorLine(SharkLine);*/
    }

    Vector3 RandomPosition()
    {
        float bw = _bw/2;
        if(difficulty < 1.5f)
        {
            float Rand =  Random.Range(0, 2) == 0 ? -(_bh/2)-0.4f : (_bh/2)+0.4f;
            return new Vector3(Random.Range(-bw, bw), Rand, 0);
        }
        else 
        {
            bool randomY = Random.Range(0, 2) == 0 ? true : false;
            if(randomY)
            {
                float RandX =  Random.Range(0, 2) == 0 ? -bw-0.4f : bw+0.4f;
                return new Vector3(RandX,Random.Range(-_bh/2, _bh/2), 0);
            }
            float Rand =  Random.Range(0, 2) == 0 ? -(_bh/2)-0.4f : (_bh/2)+0.4f;
            return new Vector3(Random.Range(-bw, bw), Rand, 0);
        }
    }

    void AssignSharkBehaviorChase (GameObject shark)
    {
        var chaseBehavior = shark.AddComponent<PerseguirTortuga>();
        chaseBehavior.playerTransform = playerTransform;
        chaseBehavior.speed =  0.9f + Mathf.Log(difficulty,7);
    }
    void AssignSharkBehaviorLine(GameObject shark)
    {    
        int spawnQuadrant = DetermineSpawnLocation(shark.transform.position); 
        float angleDegrees = DetermineAngle(spawnQuadrant);

        var straightLineBehavior = shark.AddComponent<LineaRecta>();
        straightLineBehavior.SetMoveDirection(angleDegrees);
        straightLineBehavior.difficulty = difficulty;

        straightLineBehavior.speed =  0.75f + Mathf.Log(difficulty,7);
        
        shark.AddComponent<GirarCuerpo>();
    }

    void AssignSharkSneaky(GameObject shark)
    {    
        int spawnQuadrant = DetermineSpawnLocation(shark.transform.position); 
        float angleDegrees = DetermineAngle(spawnQuadrant);

        var sneakyBehaviour = shark.AddComponent<MovimientoSigiloso>();
        sneakyBehaviour.SetMoveDirection(angleDegrees);
        sneakyBehaviour.centerPoint = playerTransform;
        //sneakyBehaviour.difficulty = difficulty;

        //sneakyBehaviour.speed =  0.75f + Mathf.Log(difficulty+0.001f,4);

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
