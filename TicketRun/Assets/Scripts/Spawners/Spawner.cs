using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private float timeToSpawn = 2.0f;
    private float timeSinceLastSpawn;

    public float difficulty = 1f;
    private float softCapDifficulty = 1.8F;
    public float difficultyIncreaseRate = 0.025f; 
    private float difficultyIncreaseInterval = 2.5f;
    private float timeSinceLastDifficultyIncrease = 0.0f;



    public Transform playerTransform;
    public Transform background;

    private float _bh, _bw;
    void Start()
    {
        _bh = background.transform.localScale.y;
        _bw = background.transform.localScale.x;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= timeToSpawn)
        {
            GenerateEntity();
            timeSinceLastSpawn = 0;
            timeToSpawn = Mathf.Max(1/difficulty, 0.4f); 
        }
        
        timeSinceLastDifficultyIncrease += Time.deltaTime;

        if (timeSinceLastDifficultyIncrease >= difficultyIncreaseInterval)
        {   
            timeSinceLastDifficultyIncrease = 0;
            difficulty = (difficulty < softCapDifficulty) ? difficulty += difficultyIncreaseRate : difficulty += difficultyIncreaseRate/2;
        }

    }

    void GenerateEntity()
    {
        if(Random.Range(0, 3) == 0 && difficulty > softCapDifficulty)
        {
            GameObject SharkLine2 = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
            AssignSharkBehaviorLine(SharkLine2);
        }
        GameObject SharkLine = Instantiate(prefabs[0], RandomPosition(), Quaternion.identity);
        SharkLine.AddComponent<MovimientoSigiloso>();
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
        chaseBehavior.speed =  0.9f + Mathf.Log(difficulty+0.001f,5);
        shark.AddComponent<GirarCuerpo>();
    }
    void AssignSharkBehaviorLine(GameObject shark)
    {
           
         
            float angleDegrees;
            int spawnQuadrant = DetermineSpawnLocation(shark.transform.position); 
            

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
            
            var straightLineBehavior = shark.AddComponent<LineaRecta>();
            straightLineBehavior.SetMoveDirection(angleDegrees);
            straightLineBehavior.difficulty = difficulty;

            straightLineBehavior.speed =  0.75f + Mathf.Log(difficulty+0.001f,4);
            

            shark.AddComponent<GirarCuerpo>();
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
