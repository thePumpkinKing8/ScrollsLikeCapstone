using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private List<Vector3> patrolPoints;
    private int currentPatrolIndex = 0;
    private Vector3 targetPosition;

    private DungeonLevelLoader dungeonLevelLoader;

    private void Start()
    {
        dungeonLevelLoader = FindObjectOfType<DungeonLevelLoader>();

        // Convert patrol points from grid to world positions
        patrolPoints = dungeonLevelLoader.levelData.patrolPoints
            .Select(p => new Vector3(p.x, transform.position.y, p.y))
            .ToList();

        // Set the first target position
        if (patrolPoints.Count > 0)
        {
            targetPosition = patrolPoints[currentPatrolIndex];
        }
    }

    private void Update()
    {
        if (patrolPoints.Count == 0) return;

        MoveTowardsPatrolPoint();
    }

    private void MoveTowardsPatrolPoint()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
            targetPosition = patrolPoints[currentPatrolIndex];
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
