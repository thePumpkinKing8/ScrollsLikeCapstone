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
    //cameras vertical rotation in degrees relative to the direction its been rotated
    private float _cameraVerticalRotation = 0;
    void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerLookEvent.AddListener(HandleLook);
        _cameraTransform = GetComponentInChildren<Camera>().transform; 
        _cameraVerticalRotation = 0;
        _cameraTransform.localEulerAngles = Vector3.zero;
    }

    private void Start()
    {
        _cameraVerticalRotation = 0;
        _cameraTransform.localEulerAngles = Vector3.zero;
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
        _xAxis = axis.x * _sensitivity;
        _yAxis = -axis.y * _sensitivity;
    }

    private void Look()
    {
        //rotate camera vertically
        _cameraVerticalRotation += _yAxis;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
        _cameraTransform.Rotate(Vector3.right * (_cameraVerticalRotation == -90 || _cameraVerticalRotation == 90 ? 0 : _yAxis));
        
        Debug.Log($"{_cameraVerticalRotation}");
      
        //dont like manually setting the camera angle but its the only way i could figure out how to clamp the rotation

        //rotate the player horizontally
        transform.Rotate(Vector3.up * _xAxis);
        _yAxis = 0;
        _xAxis = 0;
    }

    
}
