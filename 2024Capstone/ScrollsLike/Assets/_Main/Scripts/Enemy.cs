using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameManager.Instance.Player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("CardGameTest");
    }
}
