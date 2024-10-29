using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private PlayerActionsData _actions;
    private Rigidbody _rb;
    private Vector3 _horizontal;
    
    private void Awake()
    {
       
        _actions = InputManager.Instance.ActionsData;
        _rb = GetComponent<Rigidbody>();
        _actions.PlayerMoveEvent.AddListener(HandleMovment);
    }
    private void Start()
    {
        if (GameManager.Instance.Player == null)
            GameManager.Instance.Player = transform;
        else
        {
            transform.position = GameManager.Instance.PlayerPosition;
            GameManager.Instance.Player = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    public void HandleMovment(Vector2 move)
    {
        _horizontal = new Vector3(move.x, 0, move.y);
    }

    public void HandleLook(Vector2 lookDirection)
    {

    }

    private void Move()
    {
        //_rb.velocity = new Vector3(_horizontal.x * _speed, _rb.velocity.y, _horizontal.z * _speed);
        _rb.velocity = (transform.forward * (_horizontal.z * _speed)) + new Vector3(0, _rb.velocity.y, 0) + (transform.right * (_horizontal.x * _speed));
    }

    private void Look()
    {

    }
}
