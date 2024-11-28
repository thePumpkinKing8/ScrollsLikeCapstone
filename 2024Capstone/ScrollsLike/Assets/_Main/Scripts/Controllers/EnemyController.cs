using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private List<Vector3> patrolPoints;
    private Vector3 targetPosition;

    private DungeonLevelLoader dungeonLevelLoader;

    private void Start()
    {
        dungeonLevelLoader = FindObjectOfType<DungeonLevelLoader>();

        // Convert patrol points from grid to world positions
        patrolPoints = dungeonLevelLoader.levelData.patrolPoints
            .Select(p => new Vector3(p.x, transform.position.y, p.y))
            .ToList();

        // Debugging patrol points
        foreach (var point in patrolPoints)
        {
            Debug.Log($"Patrol Point: {point}, Walkable: {IsWalkable(point)}");
        }

        // Set a random starting patrol point
        if (patrolPoints.Count > 0)
        {
            targetPosition = patrolPoints[Random.Range(0, patrolPoints.Count)];
        }
    }

    private void Update()
    {
        if (patrolPoints.Count == 0) return;

        MoveTowardsPatrolPoint();
    }

    private void MoveTowardsPatrolPoint()
    {
        // Check if the enemy has reached the current target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Pick a new patrol point
            targetPosition = GetNextValidPatrolPoint();
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private Vector3 GetNextValidPatrolPoint()
    {
        Vector3 newPatrolPoint = targetPosition; // Default to the current position if no valid point is found
        int attempts = 0;

        // Attempt to find a valid point
        while (attempts < patrolPoints.Count)
        {
            Vector3 candidatePoint = patrolPoints[Random.Range(0, patrolPoints.Count)];

            if (candidatePoint != targetPosition && IsWalkable(candidatePoint))
            {
                newPatrolPoint = candidatePoint;
                break;
            }

            attempts++;
        }

        return newPatrolPoint;
    }

    private bool IsWalkable(Vector3 position)
    {
        // Convert world position to grid coordinates
        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));

        // Check boundaries
        if (gridPosition.x < 0 || gridPosition.x >= dungeonLevelLoader.levelData.levelWidth ||
            gridPosition.y < 0 || gridPosition.y >= dungeonLevelLoader.levelData.levelHeight)
        {
            return false; // Out of bounds
        }

        // Check grid data for walls or obstacles (e.g., 1 = wall)
        bool isWalkable = dungeonLevelLoader.levelData.grid[gridPosition.x, gridPosition.y] != 1;
        Debug.Log($"Grid Pos {gridPosition} is Walkable: {isWalkable}");
        return isWalkable;
    }
}
