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
    private int X = 1; 

    GameObject  LatesPad; 
    private float timeSinceLastSpawn = 0,timeToSpawn = 50f;

    void Start()
    {
        // Spawn the initial jump pad directly at the turtle's current position
         bw = background.localScale.x;
        SpawnJumpPads(-1.5f);
        maximumAltitude = turtle.transform.position.y;
        X = Random.Range(0,2) == 0 ? 1 : -1;
       
    }

    void Update()
    {
        /*timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn)
        {
            timeSinceLastSpawn = 0;
            SpawnJumpPads(LatesPad.transform.position.y);
            if(yDistance<1.1f)
            {
                yDistance+=0.01f;
            }
            
        }*/
    }


    void SpawnJumpPads(float altitude)
    {
        // Get the central position for the first jumper
        float centerX = transform.position.x;
        float moveSpeed = 1.5f;

        // Spawn three jump pads spaced apart by xRange
        for (int i = 1; i < 1000; i++)
        {
            float xPosition = Random.Range(bw/2,0.25f) * X; // Calculate x position
            X*=-1;
            Vector3 spawnPosition = new Vector3(xPosition, altitude + 3f*i, 0);
            GameObject newPad = Instantiate(jumpPadPrefab, spawnPosition, Quaternion.identity);
            Juego3 juego3 = newPad.GetComponent<Juego3>();
            juego3.setTurtle(turtle);
            if(moveSpeed > 1.470)
            {
                moveSpeed -= i/1000f;
            }
            
            juego3.setSpeed(moveSpeed);
            latestHeight = newPad.transform.position.y;
            if(i == 99)
            {
                LatesPad = newPad;
            }
            
        }
    }
}
