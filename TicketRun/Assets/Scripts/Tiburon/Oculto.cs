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
    public float chaseDuration = 20.0f;
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
        rb.velocity = transform.up * speed;

    }

    public void AvoidObstacle() {
        gameObject.layer = LayerMask.NameToLayer("Immune");
        StartCoroutine(ChangeAlpha(0.1f, "Immune"));
    }

    public void FacePlayer() {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        rb.velocity = transform.up * speed;
    }

     IEnumerator ChangeAlpha(float targetAlpha, string Layer) {
        float startAlpha = spriteRenderer.color.a;
        for (float t = 0; t < 1; t += Time.deltaTime / 1f) {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
             yield return null;
        }
        gameObject.layer = LayerMask.NameToLayer(Layer);
        yield return null;
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
            AvoidObstacle();
        }
    }

    public void ExitObstacle(Collider2D other) {
            obstacles.Remove(other.transform);
            StartCoroutine(ChangeAlpha(1.0f, "Enemy"));
            if (obstacles.Count == 0) {
                Eat.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(ChangeAlpha(1.0f, "Enemy"));
            }
    }
}
