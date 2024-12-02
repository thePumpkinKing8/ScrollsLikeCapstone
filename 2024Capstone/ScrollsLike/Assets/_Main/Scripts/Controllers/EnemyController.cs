using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float chaseThreshold = 5f;  // Distance at which the enemy starts chasing
    [SerializeField] private float patrolThreshold = 0.5f;  // Threshold to reach patrol point
    private List<Vector3> patrolPoints;
    private Vector3 targetPosition;
    private Rigidbody rb;
    private DungeonLevelLoader dungeonLevelLoader;

    private Vector3 lastPosition;
    private float stuckDistanceThreshold = 0.1f;
    private float stuckTimer;
    [SerializeField] private float stuckThreshold = 3f;
    [SerializeField] private float chaseSpeedMultiplier = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dungeonLevelLoader = FindObjectOfType<DungeonLevelLoader>();

        patrolPoints = dungeonLevelLoader.levelData.patrolPoints
            .Select(p => new Vector3(p.x, transform.position.y, p.y))
            .ToList();

        if (patrolPoints.Count > 0)
        {
            targetPosition = patrolPoints[Random.Range(0, patrolPoints.Count)];
        }

        lastPosition = transform.position;
    }

    private void Update()
    {
        if (patrolPoints.Count == 0 || GameManager.Instance.State != GameState.Dungeon) return;

        CheckIfStuck();

        if (IsPlayerInChaseRange())
        {
            ChasePlayer();
        }
        else
        {
            MoveTowardsPatrolPoint();
        }
    }

    private bool IsPlayerInChaseRange()
    {
        // Check the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.Player.position);
        return distanceToPlayer <= chaseThreshold;
    }

    private void ChasePlayer()
    {
        // Increase the speed when chasing
        targetPosition = GameManager.Instance.Player.position;

        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 velocity = direction * speed * chaseSpeedMultiplier;

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }


    private void MoveTowardsPatrolPoint()
    {
        if (Vector3.Distance(transform.position, targetPosition) < patrolThreshold)  // Increased threshold for reaching patrol points
        {
            // Reaching the patrol point
            targetPosition = GetRandomPatrolPoint();  // Get a new target point
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 nextPosition = transform.position + direction * speed * Time.deltaTime;

        rb.MovePosition(nextPosition);
    }

    private Vector3 GetRandomPatrolPoint()
    {
        return patrolPoints[Random.Range(0, patrolPoints.Count)];
    }

    private void CheckIfStuck()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        if (distanceMoved < stuckDistanceThreshold)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckThreshold)
            {
                // Enemy is considered stuck; select a new patrol point
                targetPosition = GetRandomPatrolPoint();
                ResetStuckTimer();
            }
        }
        else
        {
            // Reset the stuck timer if the enemy is moving normally
            ResetStuckTimer();
        }

        lastPosition = transform.position;
    }

    private void ResetStuckTimer()
    {
        stuckTimer = 0f;
    }
}
