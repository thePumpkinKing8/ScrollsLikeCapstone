using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };

    private Vector2Int currentDirection;
    private Vector3 targetPosition;

    private float moveTimer = 0f;
    private float moveInterval = 1f;

    private Vector2Int nextPosInGrid;
    private DungeonLevelLoader dungeonLevelLoader;
    private bool isWandering = true;

    private void Start()
    {
        dungeonLevelLoader = FindObjectOfType<DungeonLevelLoader>();

        currentDirection = directions[Random.Range(0, directions.Length)];
        nextPosInGrid = Vector2Int.FloorToInt(transform.position); 
        targetPosition = transform.position; 
    }

    private void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveInterval && isWandering)
        {
            Wander();
            moveTimer = 0f; 
        }

        Move();
    }

    void Wander()
    {
        List<Vector2Int> options = new List<Vector2Int>();

        foreach (Vector2Int dir in directions)
        {
            Vector2Int newPos = nextPosInGrid + dir;

            if (IsWalkable(newPos))
            {
                options.Add(dir);
            }
        }

        // If there are no available options, reverse direction
        if (options.Count == 0)
        {
            currentDirection = -currentDirection;
        }
        else
        {
            currentDirection = options[Random.Range(0, options.Count)]; 
        }

        Vector2Int newNextPos = nextPosInGrid + currentDirection;
        if (IsWalkable(newNextPos)) 
        {
            nextPosInGrid = newNextPos;
            targetPosition = new Vector3(nextPosInGrid.x, transform.position.y, nextPosInGrid.y);
        }
    }

    void Move()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    bool IsWalkable(Vector2Int position)
    {
        // Ensure the position is within the grid bounds
        if (position.x < 0 || position.x >= dungeonLevelLoader.levelData.levelWidth ||
            position.y < 0 || position.y >= dungeonLevelLoader.levelData.levelHeight)
        {
            return false; // Out of bounds
        }

        // Check if the tile at 'position' in the grid is not a wall ( 1 is a wall)
        return dungeonLevelLoader.levelData.grid[position.x, position.y] != 1;
    }
}
