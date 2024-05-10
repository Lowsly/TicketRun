using System.Threading;
using UnityEngine;

public class Spawner3 : MonoBehaviour
{
    public GameObject turtle; // Assign your turtle GameObject in the Inspector
    public GameObject jumpPadPrefab; // Assign the prefab for the jump pad
    public Transform background;
    public float xRange = 2.0f; // X-axis range between jump pads in subsequent spawns
    public float yDistance = 5.0f; // Y-axis distance for each new set of jump pads
    private float maximumAltitude; // To track the highest altitude reached by the turtle
    private bool initialPadSpawned = false; // To track if the initial jump pad has been spawned
    private float bw, latestHeight;

    GameObject  LatesPad; 
    private float timeSinceLastSpawn = 0,timeToSpawn = 1f;

    void Start()
    {
        // Spawn the initial jump pad directly at the turtle's current position
         bw = background.localScale.x;
        SpawnJumpPads(-1.5f);
        maximumAltitude = turtle.transform.position.y;
       
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn)
        {
            timeSinceLastSpawn = 0;
            SpawnJumpPads(LatesPad.transform.position.y);
        }
    }


    void SpawnJumpPads(float altitude)
    {
        // Get the central position for the first jumper
        float centerX = transform.position.x;

        // Spawn three jump pads spaced apart by xRange
        for (int i = 1; i < 4; i++)
        {
            float xPosition = Random.Range(-bw/2,bw/2);; // Calculate x position
            Vector3 spawnPosition = new Vector3(xPosition, altitude + 2*i, 0);
            GameObject newPad = Instantiate(jumpPadPrefab, spawnPosition, Quaternion.identity);
            newPad.GetComponent<Juego3>().setTurtle(turtle);
            latestHeight = newPad.transform.position.y;
            if(i == 3)
            {
                LatesPad = newPad;
            }
            
        }
    }
}
