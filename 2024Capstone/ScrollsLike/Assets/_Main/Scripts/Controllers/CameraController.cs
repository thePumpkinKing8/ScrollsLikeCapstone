using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerActionsData _actions;
    private Transform _cameraTransform;
    private float _xAxis;
    private float _yAxis;
    [SerializeField] private float _sensitivity = .5f;
    public float sensitivity
    {
        get
        {
            return Mathf.Abs(_sensitivity);
        }
    }
 
    void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerLookEvent.AddListener(HandleLook);
        _cameraTransform = GetComponentInChildren<Camera>().transform; 
        _cameraTransform.localEulerAngles = Vector3.zero;
    }

    private void Start()
    {
        _cameraTransform.localEulerAngles = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        
    }

    public void HandleLook(Vector2 axis)
    {
        _xAxis = axis.x * _sensitivity;
        //_yAxis = -axis.y * _sensitivity;
    }

    private void Look()
    {
        //rotate the player horizontally
        transform.Rotate(Vector3.up * _xAxis);
        //_yAxis = 0;
        _xAxis = 0;
    }

    
}
