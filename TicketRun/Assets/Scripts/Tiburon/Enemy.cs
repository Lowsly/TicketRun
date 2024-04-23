using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBehavioralEntity {
    public float speed = 5.0f;
    public float turnSpeed = 2.0f;
    private static BehaviorNode behaviorTree;
    private List<Transform> obstacles = new List<Transform>(); // List to track multiple obstacles
    private List<Transform> earlyWarnings = new List<Transform>(); // List for early warning obstacles
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        if (behaviorTree == null) {
            InitializeBehaviorTree();
        }
    }

    private void Update() {
        behaviorTree.Execute(this);
    }

    public void HandleObstacleEnter(Transform obstacle) {
        if (!obstacles.Contains(obstacle)) {
            obstacles.Add(obstacle);
        }
    }

    public void HandleObstacleExit(Transform obstacle) {
        obstacles.Remove(obstacle);
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
        Vector3 newDirection = Vector3.Lerp(transform.up, obstacleDirection, 0.1f);
        transform.up = newDirection;
    }


    public bool CheckForObstacles() {
        return obstacles.Count > 0;
    }

    public void MoveStraight() {
        rb.velocity = transform.up * speed;
    }

    public void AvoidObstacle() {
    if (obstacles.Count > 0) {
        Vector2 escapeDirection = Vector2.zero;
        Vector2 currentPosition = transform.position;

        // Calculate the average direction away from all obstacles
        foreach (Transform obstacle in obstacles) {
            Vector2 toObstacle = obstacle.position - transform.position;
            escapeDirection -= toObstacle.normalized; // Subtract to move away from the obstacle
        }

        escapeDirection /= obstacles.Count; // Average the direction
        escapeDirection.Normalize(); // Normalize to get a direction vector

        // Rotate towards the average escape direction
        float targetAngle = Mathf.Atan2(escapeDirection.y, escapeDirection.x) * Mathf.Rad2Deg - 90;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Move in the new direction
        MoveStraight();
    }
}


     private static void InitializeBehaviorTree() {
        SelectorNode root = new SelectorNode();
        SequenceNode avoidSequence = new SequenceNode();

        ConditionalNode checkObstacles = new ConditionalNode((IBehavioralEntity entity) => entity.CheckForObstacles());
        ActionNode avoidAction = new ActionNode((IBehavioralEntity entity) => { entity.AvoidObstacle(); return true; });

        avoidSequence.AddChild(checkObstacles);
        avoidSequence.AddChild(avoidAction);

        ActionNode moveStraightAction = new ActionNode((IBehavioralEntity entity) => { entity.MoveStraight(); return true; });

        root.AddChild(avoidSequence);
        root.AddChild(moveStraightAction);

        behaviorTree = root;
    }
    public void FacePlayer() {
    }

    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }
}
