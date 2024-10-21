using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMinimap : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameManager.Instance.Player.gameObject;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
