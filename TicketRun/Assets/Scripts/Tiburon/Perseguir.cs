using System.Collections.Generic;
using UnityEngine;

public class Perseguir : MonoBehaviour, IBehavioralEntity {
    public float speed = 5.0f;
    public float turnSpeed = 2.0f;
    private Transform player;  // Reference to the player's transform
    private static BehaviorNode behaviorTree;
    private List<Transform> earlyWarnings = new List<Transform>();
    private List<Transform> obstacles = new List<Transform>(); // List to track multiple obstacles
    private Rigidbody2D rb;
     private bool isAvoidingObstacles = false;  

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        if (behaviorTree == null) {
            InitializeBehaviorTree();
        }
    }

    private void Update() {
        behaviorTree.Execute(this);
        if (!isAvoidingObstacles && player != null) {
            FacePlayer();
        }
    }

    public void HandleObstacleEnter(Transform obstacle) {
        if (!obstacles.Contains(obstacle)) {
            obstacles.Add(obstacle);
            isAvoidingObstacles = true;
        }
    }

    public void HandleObstacleExit(Transform obstacle) {
        obstacles.Remove(obstacle);
        if (obstacles.Count == 0) {
            isAvoidingObstacles = false;
        }
    }

    public bool CheckForObstacles() {
        return obstacles.Count > 0;
    }

    public void HandleEarlyWarningEnter(Transform obstacle) {
    if (!earlyWarnings.Contains(obstacle)) {
        earlyWarnings.Add(obstacle);
        PreAdjustToObstacle(obstacle);
    }
}

    public void HandleEarlyWarningExit(Transform obstacle) {
        earlyWarnings.Remove(obstacle);
    }

    private void PreAdjustToObstacle(Transform obstacle) {
        // Slight adjustment towards the obstacle
        Vector3 obstacleDirection = (obstacle.position - transform.position).normalized;
        Vector3 newDirection = Vector3.Lerp(transform.up, obstacleDirection, 0.1f); // Soft adjustment
        transform.up = newDirection;
    }


   public void AvoidObstacle() {
    Transform closestObstacle = GetMostThreateningObstacle();
    if (closestObstacle != null) {
        Vector3 obstacleDirection = closestObstacle.position - transform.position;
        Vector3 playerDirection = player.position - transform.position;

        // Determine whether to turn left or right based on the player's position
        float turnDirection = Vector3.Cross(obstacleDirection.normalized, playerDirection.normalized).z;

        // Calculate the angle to the obstacle and adjust it by the shortest route
        float angleToObstacle = Vector3.SignedAngle(transform.up, obstacleDirection, Vector3.forward);
        float angleStep = turnSpeed * Time.deltaTime;
        float turnAngle = (angleToObstacle > 0) ? -angleStep : angleStep;
        if (turnDirection > 0) {
            turnAngle = Mathf.Abs(turnAngle);  // Turn right
        } else {
            turnAngle = -Mathf.Abs(turnAngle); // Turn left
        }

        transform.Rotate(0, 0, turnAngle);
    }
    MoveStraight();
}

public void MoveStraight() {
    rb.velocity = transform.up * speed;
}

// Call this method once obstacles are cleared
public void SmoothTransitionToChasing() {
    if (!isAvoidingObstacles && player != null) {
        FacePlayer();  // Ensure this method gradually adjusts the rotation
    }
}

public void FacePlayer() {
    if (player != null) {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}


    

    private Transform GetMostThreateningObstacle() {
        Transform mostThreatening = null;
        float minDistance = float.MaxValue;
        foreach (var obs in obstacles) {
            float distance = Vector3.Distance(transform.position, obs.position);
            if (distance < minDistance) {
                minDistance = distance;
                mostThreatening = obs;
            }
        }
        return mostThreatening;
    }

     private static void InitializeBehaviorTree() {
        SelectorNode root = new SelectorNode();
        SequenceNode avoidSequence = new SequenceNode();

        ConditionalNode checkObstacles = new ConditionalNode((IBehavioralEntity entity) => entity.CheckForObstacles());
        ActionNode avoidAction = new ActionNode((IBehavioralEntity entity) => { entity.AvoidObstacle(); return true; });

        avoidSequence.AddChild(checkObstacles);
        avoidSequence.AddChild(avoidAction);

        SequenceNode chaseSequence = new SequenceNode();
        ConditionalNode noObstacles = new ConditionalNode((IBehavioralEntity entity) => !entity.CheckForObstacles());
        ActionNode chasePlayerAction = new ActionNode((IBehavioralEntity entity) => { entity.MoveStraight(); return true; });

        chaseSequence.AddChild(noObstacles);
        chaseSequence.AddChild(chasePlayerAction);

        root.AddChild(avoidSequence);
        root.AddChild(chaseSequence);

        behaviorTree = root;
    }

}
