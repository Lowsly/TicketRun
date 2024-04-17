using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 5.0f;
    public float turnSpeed = 2.0f; // Adjust the turn speed for smoother transitions
    private Rigidbody2D rb; // Reference to the Rigidbody component
    private static BehaviorNode behaviorTree;
    private bool isObstacleNear = false;
    private Vector3 originalDirection;
    private Transform obstacleTransform; // Keep track of the obstacle transform

    private void Awake() {
        rb = GetComponent<Rigidbody2D>(); // Ensure there's a Rigidbody2D component attached
        originalDirection = transform.up; // Initialize the original direction
        if (behaviorTree == null) {
            InitializeBehaviorTree();
        }
    }

    private void Update() {
        behaviorTree.Execute(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Obstacle")) {
            isObstacleNear = true;
            obstacleTransform = other.transform;
            Debug.Log("Obstacle detected: " + other.name);
            if (Vector3.Distance(transform.position, obstacleTransform.position) < 1f) { // Adjust the detection distance to your game's scale
                originalDirection = transform.up; // Update the original direction upon encountering an obstacle
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Obstacle")) {
            isObstacleNear = false;
            obstacleTransform = null;
        }
    }

    public bool CheckForObstacles() {
        return isObstacleNear;
    }

    public void MoveStraight() {
        Vector2 moveDirection = transform.up * speed;
        rb.velocity = moveDirection;
    }

    public void AvoidObstacle() {
        if (obstacleTransform != null) {
            Vector3 obstacleDirection = obstacleTransform.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, obstacleDirection, Vector3.forward);
            float turnAngle = angle > 0 ? -turnSpeed : turnSpeed;

            // Only apply rotation if not aligned with original direction
            if (Mathf.Abs(angle) > 10) { // Threshold angle to avoid small oscillations
                Quaternion rotation = Quaternion.Euler(0, 0, turnAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5); // Smooth rotation
            } else {
                transform.up = Vector3.Lerp(transform.up, originalDirection, Time.deltaTime * 5); // Smoothly align to original direction
            }
        }
        MoveStraight(); // Continue moving in the new direction
    }

    private static void InitializeBehaviorTree() {
        SelectorNode root = new SelectorNode();
        SequenceNode avoidSequence = new SequenceNode();

        ConditionalNode checkObstacles = new ConditionalNode((enemy) => enemy.CheckForObstacles());
        ActionNode avoidAction = new ActionNode((enemy) => { enemy.AvoidObstacle(); return true; });

        avoidSequence.AddChild(checkObstacles);
        avoidSequence.AddChild(avoidAction);

        ActionNode moveStraightAction = new ActionNode((enemy) => { enemy.MoveStraight(); return true; });

        root.AddChild(avoidSequence);
        root.AddChild(moveStraightAction);

        behaviorTree = root;
    }
}
