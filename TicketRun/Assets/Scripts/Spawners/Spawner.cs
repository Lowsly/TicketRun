using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public float softCapDifficulty = 1.5F;
    public float difficulty = 1f;
    
    private SimpleBehaviorTree behaviorTree;

    void Start()
    {
        // Initialize the behavior tree
        ConditionNode condition = new ConditionNode(() => difficulty > softCapDifficulty);
        ActionNode action = new ActionNode(SpawnEnemy);
        behaviorTree = new SimpleBehaviorTree(condition, action);

        // Optionally, setup a recurring spawn check
        StartCoroutine(RecurringCheck());
    }

    void Update()
    {
        // Update difficulty over time or via game events
        difficulty += Time.deltaTime * 0.1f; // Example difficulty increase
    }

    IEnumerator RecurringCheck()
    {
        while (true)
        {
            behaviorTree.Tick(); // Execute the behavior tree
            yield return new WaitForSeconds(1); // Wait for 1 second before checking again
        }
    }

    void SpawnEnemy()
    {
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Spawned Enemy");
    }
}
