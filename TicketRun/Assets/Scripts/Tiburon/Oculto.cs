using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Oculto : MonoBehaviour, IBehavioralEntity {
    public float speed = 5.0f;
    public float turnSpeed = 2.0f;
    public SpriteRenderer spriteRenderer, particles; // Reference to the SpriteRenderer
    public GameObject Eat; 
    private Transform player; // Reference to the player's transform

    private Rigidbody2D rb;
    private static BehaviorNode behaviorTree;
    private List<Transform> obstacles = new List<Transform>();
    private float chaseTimer = 0f;
    public float chaseDuration = 7.0f;
    private bool isChasing = true;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (behaviorTree == null) {
            InitializeBehaviorTree();
        }
    }

    void Update() {
        behaviorTree.Execute(this);
        UpdateChasing();
    }

    private void UpdateChasing() {
        if (isChasing) {
            FacePlayer();
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseDuration) {
                isChasing = false;
            }
        }
    }

    public bool CheckForObstacles() {
        return obstacles.Count > 0;
    }

    public void MoveStraight() {
        if (!isChasing) {
            rb.velocity = transform.up * speed;
        }
    }

    public void AvoidObstacle() {
        gameObject.layer = LayerMask.NameToLayer("Immune");
        StartCoroutine(ChangeAlpha(0.1f));
    }

    public void FacePlayer() {
        if (isChasing) {
            rb.velocity = transform.up * speed;
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    IEnumerator ChangeAlpha(float targetAlpha) {
        float elapsedTime = 0.0f;
        float duration = 3.5f; // Duration in seconds to change alpha
        float startAlpha = spriteRenderer.color.a;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, 0.4f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            particles.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private static void InitializeBehaviorTree() {
        SelectorNode root = new SelectorNode();
        SequenceNode obstacleSequence = new SequenceNode();

        ConditionalNode checkObstacles = new ConditionalNode(entity => entity.CheckForObstacles());
        ActionNode avoidObstacleAction = new ActionNode(entity => { entity.AvoidObstacle(); return true; });

        obstacleSequence.AddChild(checkObstacles);
        obstacleSequence.AddChild(avoidObstacleAction);

        SequenceNode chaseSequence = new SequenceNode();
        ConditionalNode noObstacles = new ConditionalNode(entity => !entity.CheckForObstacles());
        ActionNode chaseAction = new ActionNode(entity => { entity.MoveStraight(); return true; });

        chaseSequence.AddChild(noObstacles);
        chaseSequence.AddChild(chaseAction);

        root.AddChild(obstacleSequence);
        root.AddChild(chaseSequence);

        behaviorTree = root;
    }

    public void UpdateObstacle(Collider2D other) {
        if (!obstacles.Contains(other.transform)) {
            Eat.SetActive(false);
            obstacles.Add(other.transform);
        }
    }

    public void ExitObstacle(Collider2D other) {
            obstacles.Remove(other.transform);
            StartCoroutine(ChangeAlpha(1.0f));
            if (obstacles.Count == 0) {
                Eat.SetActive(true);
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                StopAllCoroutines();
                StartCoroutine(ChangeAlpha(1.0f));
            }
    }
}
