using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private float timeToSpawn = 1.0f;
    private float timeSinceLastSpawn;
    public Transform playerTransform;
    public Transform background;

    private float _bh, _bw;
    void Start()
    {
        _bh = background.transform.localScale.x;
        _bw = background.transform.localScale.y;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= timeToSpawn)
        {
            GenerateEntity();
            timeSinceLastSpawn = 0;
            timeToSpawn = Mathf.Max(timeToSpawn * 0.9f, 1.0f); // Example of decreasing spawn time
        }
    }

    void GenerateEntity()
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[randomIndex];
        GameObject entity = Instantiate(selectedPrefab, RandomPosition(), Quaternion.identity);

        if (entity.CompareTag("Shark"))
        {
            AssignSharkBehavior(entity);
        }
    }

    Vector3 RandomPosition()
    {
        float Rand =  Random.Range(0, 2) == 0 ? -(_bh/5) : (_bh/5);
        return new Vector3(Random.Range(-_bw/2, _bw/2), Rand, 0);
    }

    void AssignSharkBehavior(GameObject shark)
    {
        if (Random.Range(0, 2) == 0) // 50% chance
        {
            var chaseBehavior = shark.AddComponent<PerseguirTortuga>();
            chaseBehavior.playerTransform = playerTransform;
        }
        else
        {   
            float angleDegrees;
            int spawnQuadrant = DetermineSpawnQuadrant(shark.transform.position); 

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
                default:
                    angleDegrees = Random.Range(0, 360); 
                    break;
            }
            Debug.Log(angleDegrees);
            var straightLineBehavior = shark.AddComponent<LineaRecta>();
            straightLineBehavior.SetMoveDirection(angleDegrees);
        }
    }
    int DetermineSpawnQuadrant(Vector3 position)
    {
        if (position.x <= 0)
        {
            if (position.y < 0)
                return 2; // Bottom-Left
            else
                return 0; // Top-Left
        }
        else
        {
            if (position.y < 0)
                return 3; // Bottom-Right
            else
                return 1; // Top-Right
        }
    }

}
