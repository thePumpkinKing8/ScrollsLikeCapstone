using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDeck _enemyDeck;
    public EnemyDeck EnemiesDeck { get { return _enemyDeck; } }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            transform.LookAt(GameManager.Instance.Player.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GoToCombat(EnemiesDeck, gameObject);
        }
    }
}
