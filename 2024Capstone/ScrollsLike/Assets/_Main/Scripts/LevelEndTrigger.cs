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
    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            transform.LookAt(GameManager.Instance.Player.position);
        }
    }
}
