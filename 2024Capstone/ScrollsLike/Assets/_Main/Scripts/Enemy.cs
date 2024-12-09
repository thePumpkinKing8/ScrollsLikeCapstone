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
        Debug.Log("spawn");
        transform.position = new Vector3(transform.position.x, .75f, transform.position.z);
    }

    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            transform.LookAt(GameManager.Instance.Player.position);
        }
        if(transform.position.y < .75f)
        {
            transform.position = new Vector3(transform.position.x, .75f, transform.position.z);
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
