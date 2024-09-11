using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileMovement : MonoBehaviour
{
    private void Awake()
    {
        InputManager.Instance.ActionsData.PlayerMoveEvent.AddListener(HandleMove);
        InputManager.Instance.ActionsData.PlayerLookEvent.AddListener(HandleLook);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMove(Vector2 direction)//should probably have this work in tandem with a tile manager, translating the object towards the targeted tiles center
    {
        if(direction == Vector2.zero) return;

        Debug.Log(transform.forward * Mathf.Sign(direction.y));
        transform.Translate(new Vector3(direction.x, 0, direction.y));
    }

    public void HandleLook(Vector2 direction)
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (90 * direction.x));
    }
}
