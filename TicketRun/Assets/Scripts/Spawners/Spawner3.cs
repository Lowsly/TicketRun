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
    private float bw;

    void Start()
    {
        // Spawn the initial jump pad directly at the turtle's current position
         bw = background.localScale.x;
        SpawnJumpPads(-0.52f);
       
    }

    void Update()
    {
        // Check if the turtle has reached 1.5 times the previous maximum altitude
        if (turtle.transform.position.y > maximumAltitude * 1.5f)
        {
            // Update the maximum altitude
            maximumAltitude = turtle.transform.position.y;

            // Spawn a new set of three jump pads at the new altitude
           
        }
    }


    void SpawnJumpPads(float altitude)
    {
        // Get the central position for the first jumper
        float centerX = transform.position.x;

        // Spawn three jump pads spaced apart by xRange
        for (int i = 0; i < 3; i++)
        {
            float xPosition = randomX(); // Calculate x position
            Debug.Log(xPosition);
            Vector3 spawnPosition = new Vector3(xPosition, altitude+i, 0);
            GameObject newPad = Instantiate(jumpPadPrefab, spawnPosition, Quaternion.identity);
            newPad.GetComponent<Juego3>().setTurtle(turtle);
            // If the jump pads need to track or interact with the turtle in some way, you could do it here
        }
    }
    float randomX()
    {
        return Random.Range(-bw/2,bw/2);
    }
}
