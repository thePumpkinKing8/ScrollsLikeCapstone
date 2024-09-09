using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerActionsData _actions;
    private Transform _cameraTransform;
    private float _xAxis;
    private float _yAxis;
    //cameras vertical rotation in degrees relative to the direction its been rotated
    private float _cameraVerticalRotation = 0;
    void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerLookEvent.AddListener(HandleLook);
        _cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Look();
    }

    public void HandleLook(Vector2 axis)
    {
        _xAxis = axis.x;
        _yAxis = -axis.y;
    }

    private void Look()
    {
        //rotate camera vertically
        _cameraVerticalRotation += _yAxis;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraVerticalRotation;

        //rotate the player horizontally
        transform.Rotate(Vector3.up * _xAxis);
    }
}
