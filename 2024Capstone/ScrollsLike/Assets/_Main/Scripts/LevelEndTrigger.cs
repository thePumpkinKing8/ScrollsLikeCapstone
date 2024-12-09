using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ShowRestUI();
        }
    }
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
    }
    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            transform.LookAt(GameManager.Instance.Player.position);
        }
    }
}
