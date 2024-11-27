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

        // Set a random starting patrol point
        if (patrolPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, patrolPoints.Count);
            targetPosition = patrolPoints[randomIndex];
        }
    }

    private void Update()
    {
        if (patrolPoints.Count == 0) return;
        if(GameManager.Instance.State == GameState.Dungeon)
            MoveTowardsPatrolPoint();
    }

    private void MoveTowardsPatrolPoint()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Pick a random new patrol point as the target
            targetPosition = patrolPoints[Random.Range(0, patrolPoints.Count)];
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
