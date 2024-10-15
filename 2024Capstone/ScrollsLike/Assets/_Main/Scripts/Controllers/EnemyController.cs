using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 5f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float waitTime = 2f;
    private Vector3 centerPoint;
    private Vector3 targetPoint;
    private bool isWaiting = false;

    private void Start()
    {
        centerPoint = transform.position;
        ChooseRandomPoint();
    }

    private void Update()
    {
        if (!isWaiting)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Move the enemy towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

        // Check if the enemy has reached the target point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            StartCoroutine(WaitBeforeNextMove());
        }
    }

    private void ChooseRandomPoint()
    {
        // Generate a random point within the patrol radius
        Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
        targetPoint = new Vector3(centerPoint.x + randomPoint.x, transform.position.y, centerPoint.z + randomPoint.y);
    }

    private IEnumerator WaitBeforeNextMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        ChooseRandomPoint();
        isWaiting = false;
    }
}
