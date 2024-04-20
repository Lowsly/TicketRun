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

    public bool CheckForObstacles() {
        return obstacles.Count > 0;
    }

    public void MoveStraight() {
        rb.velocity = transform.up * speed;
    }

    public void AvoidObstacle() {
        Transform closestObstacle = GetMostThreateningObstacle();
        if (closestObstacle != null) {
            Vector3 obstacleDirection = closestObstacle.position - transform.position;
            float angleToObstacle = Vector3.SignedAngle(transform.up, obstacleDirection, Vector3.forward);
            float angleStep =  turnSpeed * Time.deltaTime;
            float turnAngle = angleToObstacle > 0 ? -angleStep : angleStep;
            transform.Rotate(0, 0, turnAngle);
        }
        MoveStraight(); // Continue moving in the new direction
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
}
